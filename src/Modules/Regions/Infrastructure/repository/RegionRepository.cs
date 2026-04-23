using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.repository;

public sealed class RegionRepository : IRegionRepository
{
    private readonly AppDbContext _context;

    public RegionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Region?> GetByIdAsync(RegionId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<RegionEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value && x.IsActive, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Region>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<RegionEntity>()
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Region> AddAsync(Region entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Region.CreateNew para insertar.");
        }

        var e = new RegionEntity
        {
            Name = entity.Name.Value,
            Type = entity.Type.Value,
            CountryId = entity.CountryId.Value,
        };
        _context.Set<RegionEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Region entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<RegionEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe región {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        e.Type = entity.Type.Value;
        e.CountryId = entity.CountryId.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(RegionId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<RegionEntity>().FirstOrDefaultAsync(
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

    private static Region ToDomain(RegionEntity e)
    {
        return Region.Create(
            RegionId.Create(e.Id),
            RegionName.Create(e.Name ?? string.Empty),
            RegionType.Create(e.Type ?? string.Empty),
            RegionCuntryId.Create(e.CountryId)
        );
    }
}
