using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Infrastructure.repository;

public sealed class MilesTransactionRepository : IMilesTransactionRepository
{
    private readonly AppDbContext _context;

    public MilesTransactionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MilesTransaction> AddAsync(MilesTransaction entity, CancellationToken cancellationToken = default)
    {
        var e = new MilesTransactionEntity
        {
            ClientId = entity.ClientId.Value,
            ReservationId = entity.ReservationId?.Value,
            Amount = entity.Amount,
            TransactionType = entity.TransactionType,
            TransactionDate = entity.TransactionDate
        };

        _context.Set<MilesTransactionEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        
        return ToDomain(e);
    }

    public async Task<IReadOnlyList<MilesTransaction>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<MilesTransactionEntity>()
            .AsNoTracking()
            .Where(x => x.ClientId == clientId)
            .ToListAsync(cancellationToken);

        return list.Select(ToDomain).ToList();
    }

    private static MilesTransaction ToDomain(MilesTransactionEntity e)
    {
        var clientObjId = ClientId.Create(e.ClientId);
        var reservationObjId = e.ReservationId.HasValue ? ReservationId.Create(e.ReservationId.Value) : null;

        var type = typeof(MilesTransaction);
        var ctor = type.GetConstructors(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).FirstOrDefault();
        
        if(ctor != null)
        {
#pragma warning disable CS8601
            return (MilesTransaction)ctor.Invoke([e.Id, clientObjId, reservationObjId, e.Amount, e.TransactionType, e.TransactionDate])!;
#pragma warning restore CS8601
        }

        throw new InvalidOperationException("No se pudo reconstruir MilesTransaction desde la BD");
    }
}
