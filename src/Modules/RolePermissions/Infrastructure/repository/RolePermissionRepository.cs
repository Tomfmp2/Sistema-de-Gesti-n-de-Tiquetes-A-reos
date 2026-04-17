using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Infrastructure.repository;

public sealed class RolePermissionRepository : IRolePermissionRepository
{
    private readonly AppDbContext _context;

    public RolePermissionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RolePermission?> GetByIdAsync(
        RolePermissionId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<RolePermissionEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<RolePermission>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<RolePermissionEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<RolePermission> AddAsync(
        RolePermission entity,
        CancellationToken cancellationToken = default
    )
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use RolePermission.CreateNew para insertar.");
        }

        var e = new RolePermissionEntity
        {
            RoleId = entity.RoleId.Value,
            PermissionId = entity.PermissionId.Value,
        };
        _context.Set<RolePermissionEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(RolePermission entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<RolePermissionEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe rol-permiso {entity.Id.Value}.");
        }

        e.RoleId = entity.RoleId.Value;
        e.PermissionId = entity.PermissionId.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(RolePermissionId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<RolePermissionEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<RolePermissionEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static RolePermission ToDomain(RolePermissionEntity e)
    {
        return RolePermission.Create(
            RolePermissionId.Create(e.Id),
            RolePermissionRoleId.Create(e.RoleId),
            RolePermissionPermissionId.Create(e.PermissionId)
        );
    }
}
