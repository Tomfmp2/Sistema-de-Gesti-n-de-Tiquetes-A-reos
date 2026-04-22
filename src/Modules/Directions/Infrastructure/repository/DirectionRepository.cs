using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.repository;

public sealed class DirectionRepository : IDirectionRepository
{
    private readonly AppDbContext _context;

    public DirectionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Direction?> GetByIdAsync(
        DirectionId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<AddressEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Direction>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<AddressEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Direction> AddAsync(Direction entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Direction.CreateNew para insertar.");
        }

        var e = new AddressEntity
        {
            CityId = entity.CityId.Value,
            StreetTypeId = entity.StreetTypeId.Value,
            StreetName = entity.StreetName.Value,
            StreetNumber = entity.Number.Value,
            Complement = entity.Complement,
            PostalCode = entity.PostalCode,
        };
        _context.Set<AddressEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Direction entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<AddressEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe dirección {entity.Id.Value}.");
        }

        e.CityId = entity.CityId.Value;
        e.StreetTypeId = entity.StreetTypeId.Value;
        e.StreetName = entity.StreetName.Value;
        e.StreetNumber = entity.Number.Value;
        e.Complement = entity.Complement;
        e.PostalCode = entity.PostalCode;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(DirectionId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<AddressEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<AddressEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Direction ToDomain(AddressEntity e)
    {
        return Direction.Create(
            DirectionId.Create(e.Id),
            DirectionCityId.Create(e.CityId),
            DirectionStreetTypeId.Create(e.StreetTypeId),
            DirectionNameStreet.Create(e.StreetName ?? string.Empty),
            DirectionNumber.Create(e.StreetNumber ?? string.Empty),
            e.Complement,
            e.PostalCode
        );
    }
}
