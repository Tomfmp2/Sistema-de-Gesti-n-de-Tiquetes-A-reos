using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.repository;

public sealed class PassengerTypeRepository : IPassengerTypeRepository
{
    private readonly AppDbContext _context;

    public PassengerTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PassengerType?> GetByIdAsync(
        PassengerTypeId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<PassengerTypeEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<PassengerType>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<PassengerTypeEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<PassengerType> AddAsync(
        PassengerType entity,
        CancellationToken cancellationToken = default
    )
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use PassengerType.CreateNew para insertar.");
        }

        var e = new PassengerTypeEntity
        {
            Name = entity.Name.Value,
            MinAge = entity.MinAge,
            MaxAge = entity.MaxAge,
        };
        _context.Set<PassengerTypeEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(PassengerType entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<PassengerTypeEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe tipo de pasajero {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        e.MinAge = entity.MinAge;
        e.MaxAge = entity.MaxAge;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PassengerTypeId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<PassengerTypeEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<PassengerTypeEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static PassengerType ToDomain(PassengerTypeEntity e)
    {
        return PassengerType.Create(
            PassengerTypeId.Create(e.Id),
            PassengerTypeName.Create(e.Name ?? string.Empty),
            e.MinAge,
            e.MaxAge
        );
    }
}
