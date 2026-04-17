using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.repository;

public sealed class ReservationFlightRepository : IReservationFlightRepository
{
    private readonly AppDbContext _context;

    public ReservationFlightRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReservationFlight?> GetByIdAsync(ReservationFlightId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<ReservationFlightEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<ReservationFlight>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<ReservationFlightEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<ReservationFlight> AddAsync(ReservationFlight entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new ReservationFlightEntity
        {
            ReservationId = entity.ReservationId.Value,
    FlightId = entity.FlightId.Value,
    PartialValue = entity.PartialValue.Value,
        };
        _context.Set<ReservationFlightEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(ReservationFlight entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<ReservationFlightEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe reservationflight {entity.Id.Value}.");
        }

        e.ReservationId = entity.ReservationId.Value;
e.FlightId = entity.FlightId.Value;
e.PartialValue = entity.PartialValue.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ReservationFlightId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<ReservationFlightEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<ReservationFlightEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static ReservationFlight ToDomain(ReservationFlightEntity e)
    {
        return ReservationFlight.Create(
            ReservationFlightId.Create(e.Id),
    ReservationFlightReservationId.Create(e.ReservationId),
    ReservationFlightFlightId.Create(e.FlightId),
    ReservationFlightPartialValue.Create(e.PartialValue)
        );
    }
}
