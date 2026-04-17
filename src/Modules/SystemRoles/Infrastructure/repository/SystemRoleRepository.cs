using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.repository;

public sealed class SystemRoleRepository : ISystemRoleRepository
{
    private readonly AppDbContext _context;

    public SystemRoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SystemRole?> GetByIdAsync(
        SystemRoleId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<SystemRoleEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<SystemRole>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<SystemRoleEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<SystemRole> AddAsync(SystemRole entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use SystemRole.CreateNew para insertar.");
        }

        var e = new SystemRoleEntity { Name = entity.Name.Value, Description = entity.Description };
        _context.Set<SystemRoleEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(SystemRole entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<SystemRoleEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe rol {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        e.Description = entity.Description;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SystemRoleId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<SystemRoleEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<SystemRoleEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static SystemRole ToDomain(SystemRoleEntity e)
    {
        return SystemRole.Create(
            SystemRoleId.Create(e.Id),
            SystemRoleName.Create(e.Name ?? string.Empty),
            e.Description
        );
    }
}
