using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.repository;

public sealed class FlightRoleRepository : IFlightRoleRepository
{
    private readonly AppDbContext _context;

    public FlightRoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FlightRole?> GetByIdAsync(FlightRoleId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var entity = await _context.Set<FlightRoleEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);

        return entity is null ? null : ToDomain(entity);
    }

    public async Task<IReadOnlyList<FlightRole>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<FlightRoleEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<FlightRole> AddAsync(FlightRole entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use FlightRole.Create para insertar.");
        }

        var model = new FlightRoleEntity
        {
            Name = entity.Name.Value
        };

        _context.Set<FlightRoleEntity>().Add(model);
        await _context.SaveChangesAsync(cancellationToken);

        return ToDomain(model);
    }

    public async Task UpdateAsync(FlightRole entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var model = await _context.Set<FlightRoleEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken);

        if (model is null)
        {
            throw new InvalidOperationException($"No existe flight role {entity.Id.Value}.");
        }

        model.Name = entity.Name.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FlightRoleId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var model = await _context.Set<FlightRoleEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken);

        if (model is null)
        {
            return;
        }

        _context.Set<FlightRoleEntity>().Remove(model);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static FlightRole ToDomain(FlightRoleEntity entity)
    {
        return FlightRole.Reconstitute(
            FlightRoleId.Create(entity.Id),
            FlightRoleName.Create(entity.Name)
        );
    }
}
