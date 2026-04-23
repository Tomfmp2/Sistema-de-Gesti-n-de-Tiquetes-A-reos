using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.repository;

public sealed class CityRepository : ICityRepository
{
    private readonly AppDbContext _context;

    public CityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<City?> GetByIdAsync(CityId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<CityEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value && x.IsActive, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<City>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<CityEntity>()
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<City> AddAsync(City entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use City.CreateNew para insertar.");
        }

        var e = new CityEntity
        {
            Name = entity.Name.Value,
            RegionId = entity.RegionId.Value,
        };
        _context.Set<CityEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(City entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<CityEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe ciudad {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        e.RegionId = entity.RegionId.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CityId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<CityEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        e.IsActive = false;
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static City ToDomain(CityEntity e)
    {
        return City.Create(
            CityId.Create(e.Id),
            CityName.Create(e.Name ?? string.Empty),
            CityRegionId.Create(e.RegionId)
        );
    }
}
