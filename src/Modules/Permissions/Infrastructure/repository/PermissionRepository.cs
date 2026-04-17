using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.repository;

public sealed class PermissionRepository : IPermissionRepository
{
    private readonly AppDbContext _context;

    public PermissionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Permission?> GetByIdAsync(
        PermissionId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<PermissionEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Permission>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<PermissionEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Permission> AddAsync(Permission entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Permission.CreateNew para insertar.");
        }

        var e = new PermissionEntity { Name = entity.Name.Value, Description = entity.Description };
        _context.Set<PermissionEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Permission entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<PermissionEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe permiso {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        e.Description = entity.Description;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PermissionId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<PermissionEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<PermissionEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Permission ToDomain(PermissionEntity e)
    {
        return Permission.Create(
            PermissionId.Create(e.Id),
            PermissionName.Create(e.Name ?? string.Empty),
            e.Description
        );
    }
}
