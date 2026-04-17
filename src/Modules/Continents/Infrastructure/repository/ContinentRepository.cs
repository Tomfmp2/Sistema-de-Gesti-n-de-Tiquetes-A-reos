using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.repository;

public sealed class ContinentRepository : IContinentRepository
{
    private readonly AppDbContext _context;

    public ContinentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Continent?> GetByIdAsync(
        ContinentId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<ContinentEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Continent>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<ContinentEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Continent> AddAsync(Continent entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Continent.CreateNew para insertar.");
        }

        var e = new ContinentEntity { Name = entity.Name.Value };
        _context.Set<ContinentEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Continent entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<ContinentEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe continente {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ContinentId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<ContinentEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<ContinentEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Continent ToDomain(ContinentEntity e)
    {
        return Continent.Create(
            ContinentId.Create(e.Id),
            ContinentName.Create(e.Name ?? string.Empty)
        );
    }
}
