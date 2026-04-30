using Microsoft.EntityFrameworkCore;
using System.Linq;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Application.DTOs;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Domain.Enum;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Application.Services;

public sealed class MilesTransactionService : IMilesTransactionService
{
    private readonly IMilesTransactionRepository _repository;
    private readonly AppDbContext _context;

    public MilesTransactionService(IMilesTransactionRepository repository, AppDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public async Task<bool> ClientExistsAsync(int clientId, CancellationToken cancellationToken = default)
    {
        return await _context.Clients.AnyAsync(c => c.Id == clientId, cancellationToken);
    }

    public async Task AccumulateMilesAsync(int clientId, int reservationId, decimal milesAmount, CancellationToken cancellationToken = default)
    {
        var transaction = MilesTransaction.CreateAccumulation(
            ClientId.Create(clientId),
            ReservationId.Create(reservationId),
            milesAmount
        );

        await _repository.AddAsync(transaction, cancellationToken);
    }

    public async Task RevertMilesForReservationAsync(int reservationId, CancellationToken cancellationToken = default)
    {
        // Buscar transacciones de acumulación para esta reserva
        var accumulations = await _context.MilesTransactions
            .Where(t => t.ReservationId == reservationId && t.TransactionType == TransactionType.Accumulation)
            .ToListAsync(cancellationToken);

        foreach (var acc in accumulations)
        {
            // Creamos una transacción de "Redención" (negativa) por el mismo monto para anularla
            var reversal = MilesTransaction.CreateRedemption(
                ClientId.Create(acc.ClientId),
                ReservationId.Create(reservationId),
                acc.Amount // CreateRedemption internamente negará el monto si es positivo, o lo usará tal cual.
            );
            
            // Si el monto de la acumulación es positivo, la redención será negativa.
            // En este dominio, MilesTransaction.CreateRedemption(..., amount) hace Amount = -amount.
            
            await _repository.AddAsync(reversal, cancellationToken);
        }
    }

    public async Task RedeemMilesAsync(int clientId, int reservationId, decimal milesAmount, CancellationToken cancellationToken = default)
    {
        // Bloqueo transaccional o lectura inicial
        var currentBalance = await GetClientBalanceAsync(clientId, cancellationToken);

        if (currentBalance < milesAmount)
        {
            throw new InvalidOperationException("Saldo de millas insuficiente para redimir.");
        }

        var transaction = MilesTransaction.CreateRedemption(
            ClientId.Create(clientId),
            ReservationId.Create(reservationId),
            milesAmount
        );

        await _repository.AddAsync(transaction, cancellationToken);
    }

    public async Task<decimal> GetClientBalanceAsync(int clientId, CancellationToken cancellationToken = default)
    {
        // Calcular el saldo como la suma de todas las transacciones (acumulación +, redención -)
        return await _context.MilesTransactions
            .Where(t => t.ClientId == clientId)
            .SumAsync(t => t.Amount, cancellationToken);
    }

    // --- Reportes Analíticos con LINQ Nivel 4 ---

    public async Task<List<ClientMilesDTO>> GetTopAccumulatorsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.MilesTransactions
            .AsNoTracking()
            .Where(t => t.TransactionType == TransactionType.Accumulation)
            .GroupBy(t => new { t.ClientId, FirstName = t.Client!.Person!.FirstName, LastName = t.Client.Person.LastName })
            .Select(g => new ClientMilesDTO
            {
                ClientId = g.Key.ClientId,
                ClientName = $"{g.Key.FirstName} {g.Key.LastName}",
                TotalAccumulated = g.Sum(t => t.Amount)
            })
            .OrderByDescending(c => c.TotalAccumulated)
            .Take(10)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<ClientRedemptionDTO>> GetTopRedeemersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.MilesTransactions
            .AsNoTracking()
            .Where(t => t.TransactionType == TransactionType.Redemption)
            .GroupBy(t => new { t.ClientId, FirstName = t.Client!.Person!.FirstName, LastName = t.Client.Person.LastName })
            .Select(g => new ClientRedemptionDTO
            {
                ClientId = g.Key.ClientId,
                ClientName = $"{g.Key.FirstName} {g.Key.LastName}",
                TotalRedeemed = g.Sum(t => Math.Abs(t.Amount))
            })
            .OrderByDescending(c => c.TotalRedeemed)
            .Take(10)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AirlineLoyaltyDTO>> GetTopAirlinesByLoyaltyAsync(CancellationToken cancellationToken = default)
    {
        return await _context.MilesTransactions
            .AsNoTracking()
            .Where(t => t.TransactionType == TransactionType.Accumulation && t.ReservationId != null)
            .SelectMany(t => t.Reservation!.ReservationFlights, (t, rf) => new { t.Amount, rf.Flight!.AirlineId, rf.Flight.Airline!.Name })
            .GroupBy(x => new { x.AirlineId, x.Name })
            .Select(g => new AirlineLoyaltyDTO
            {
                AirlineId = g.Key.AirlineId,
                AirlineName = g.Key.Name ?? "(sin nombre)",
                TotalMilesGranted = g.Sum(x => x.Amount)
            })
            .OrderByDescending(x => x.TotalMilesGranted)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<RouteMilesDTO>> GetTopRoutesByMilesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.MilesTransactions
            .AsNoTracking()
            .Where(t => t.TransactionType == TransactionType.Accumulation && t.ReservationId != null)
            .SelectMany(t => t.Reservation!.ReservationFlights, (t, rf) => new {
                t.Amount,
                Origin      = rf.Flight!.Route!.OriginAirport!.Name,
                Destination = rf.Flight.Route.DestinationAirport!.Name
            })
            .GroupBy(x => new { x.Origin, x.Destination })
            .Select(g => new RouteMilesDTO
            {
                Origin = g.Key.Origin,
                Destination = g.Key.Destination,
                TotalMiles = g.Sum(x => x.Amount)
            })
            .OrderByDescending(x => x.TotalMiles)
            .Take(5)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<FrequentFlyerDTO>> GetFrequentFlyersRankingAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Clients
            .AsNoTracking()
            .Select(c => new FrequentFlyerDTO
            {
                ClientId = c.Id,
                ClientName = $"{c.Person!.FirstName} {c.Person.LastName}",
                // Contar reservaciones confirmadas (Status Id = 2) que asimilamos como viaje completado en este sistema
                CompletedFlights = c.Reservations.Count(r => r.ReservationStatusId == 2),
                CurrentBalance = c.MilesTransactions.Sum(t => t.Amount)
            })
            .Where(c => c.CompletedFlights > 0)
            .OrderByDescending(c => c.CompletedFlights)
            .ThenByDescending(c => c.CurrentBalance)
            .ToListAsync(cancellationToken);
    }
}
