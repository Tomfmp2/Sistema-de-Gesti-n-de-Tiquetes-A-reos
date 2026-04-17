using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Infrastructure.repository;

public sealed class ReservationStatusTransitionRepository : IReservationStatusTransitionRepository
{
    private readonly AppDbContext _context;

    public ReservationStatusTransitionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReservationStatusTransition?> GetByIdAsync(ReservationStatusTransitionId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<ReservationStatusTransitionEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<ReservationStatusTransition>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<ReservationStatusTransitionEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<ReservationStatusTransition> AddAsync(ReservationStatusTransition entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new ReservationStatusTransitionEntity
        {
            OriginStatusId = entity.OriginStatusId.Value,
    DestinationStatusId = entity.DestinationStatusId.Value,
        };
        _context.Set<ReservationStatusTransitionEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(ReservationStatusTransition entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<ReservationStatusTransitionEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe reservationstatustransition {entity.Id.Value}.");
        }

        e.OriginStatusId = entity.OriginStatusId.Value;
e.DestinationStatusId = entity.DestinationStatusId.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ReservationStatusTransitionId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<ReservationStatusTransitionEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<ReservationStatusTransitionEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static ReservationStatusTransition ToDomain(ReservationStatusTransitionEntity e)
    {
        return ReservationStatusTransition.Create(
            ReservationStatusTransitionId.Create(e.Id),
    ReservationStatusTransitionOriginStatusId.Create(e.OriginStatusId),
    ReservationStatusTransitionDestinationStatusId.Create(e.DestinationStatusId)
        );
    }
}
