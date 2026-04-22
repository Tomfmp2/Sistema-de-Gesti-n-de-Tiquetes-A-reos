using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Infrastructure.repository;

public sealed class CheckinStatusRepository : ICheckinStatusRepository
{
    private readonly AppDbContext _context;

    public CheckinStatusRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CheckinStatus?> GetByIdAsync(CheckinStatusId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<CheckinStatusEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<CheckinStatus>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<CheckinStatusEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<CheckinStatus> AddAsync(CheckinStatus entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new CheckinStatusEntity
        {
            Name = entity.Name.Value,
        };
        _context.Set<CheckinStatusEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(CheckinStatus entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<CheckinStatusEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe checkinstatus {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CheckinStatusId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<CheckinStatusEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<CheckinStatusEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static CheckinStatus ToDomain(CheckinStatusEntity e)
    {
        ArgumentNullException.ThrowIfNull(e.Name);
        return CheckinStatus.Create(
            CheckinStatusId.Create(e.Id),
    CheckinStatusName.Create(e.Name)
        );
    }
}
