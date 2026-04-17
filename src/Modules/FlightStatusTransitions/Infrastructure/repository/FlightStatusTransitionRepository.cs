using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Infrastructure.repository;

public sealed class FlightStatusTransitionRepository : IFlightStatusTransitionRepository
{
    private readonly AppDbContext _context;

    public FlightStatusTransitionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FlightStatusTransition?> GetByIdAsync(FlightStatusTransitionId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<FlightStatusTransitionEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<FlightStatusTransition>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<FlightStatusTransitionEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<FlightStatusTransition> AddAsync(FlightStatusTransition entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new FlightStatusTransitionEntity
        {
            OriginStatusId = entity.OriginStatusId.Value,
    DestinationStatusId = entity.DestinationStatusId.Value,
        };
        _context.Set<FlightStatusTransitionEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(FlightStatusTransition entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<FlightStatusTransitionEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe flightstatustransition {entity.Id.Value}.");
        }

        e.OriginStatusId = entity.OriginStatusId.Value;
e.DestinationStatusId = entity.DestinationStatusId.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FlightStatusTransitionId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<FlightStatusTransitionEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<FlightStatusTransitionEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static FlightStatusTransition ToDomain(FlightStatusTransitionEntity e)
    {
        return FlightStatusTransition.Create(
            FlightStatusTransitionId.Create(e.Id),
    FlightStatusTransitionOriginStatusId.Create(e.OriginStatusId),
    FlightStatusTransitionDestinationStatusId.Create(e.DestinationStatusId)
        );
    }
}
