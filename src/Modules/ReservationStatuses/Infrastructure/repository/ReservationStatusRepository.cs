using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.repository;

public sealed class ReservationStatusRepository : IReservationStatusRepository
{
    private readonly AppDbContext _context;

    public ReservationStatusRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReservationStatus?> GetByIdAsync(ReservationStatusId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<ReservationStatusEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<ReservationStatus>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<ReservationStatusEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<ReservationStatus> AddAsync(ReservationStatus entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new ReservationStatusEntity
        {
            Name = entity.Name.Value,
        };
        _context.Set<ReservationStatusEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(ReservationStatus entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<ReservationStatusEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe reservationstatus {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ReservationStatusId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<ReservationStatusEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<ReservationStatusEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static ReservationStatus ToDomain(ReservationStatusEntity e)
    {
        ArgumentNullException.ThrowIfNull(e.Name);
        return ReservationStatus.Create(
            ReservationStatusId.Create(e.Id),
    ReservationStatusName.Create(e.Name)
        );
    }
}
