using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.repository;

public sealed class FlightStatusRepository : IFlightStatusRepository
{
    private readonly AppDbContext _context;

    public FlightStatusRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FlightStatus?> GetByIdAsync(FlightStatusId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<FlightStatusEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<FlightStatus>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<FlightStatusEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<FlightStatus> AddAsync(FlightStatus entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new FlightStatusEntity
        {
            Name = entity.Name.Value,
        };
        _context.Set<FlightStatusEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(FlightStatus entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<FlightStatusEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe flightstatus {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FlightStatusId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<FlightStatusEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<FlightStatusEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static FlightStatus ToDomain(FlightStatusEntity e)
    {
        ArgumentNullException.ThrowIfNull(e.Name);
        return FlightStatus.Create(
            FlightStatusId.Create(e.Id),
    FlightStatusName.Create(e.Name)
        );
    }
}
