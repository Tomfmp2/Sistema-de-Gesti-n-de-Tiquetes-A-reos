using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.repository;

public sealed class CountryRepository : ICountryRepository
{
    private readonly AppDbContext _context;

    public CountryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Country?> GetByIdAsync(CountryId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<CountryEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<CountryEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Country> AddAsync(Country entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Country.CreateNew para insertar.");
        }

        var e = new CountryEntity
        {
            Name = entity.Name.Value,
            CodeIso = entity.CodeIso.Value,
            ContinentId = entity.ContinentId.Value,
        };
        _context.Set<CountryEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Country entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<CountryEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe país {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        e.CodeIso = entity.CodeIso.Value;
        e.ContinentId = entity.ContinentId.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CountryId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<CountryEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<CountryEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Country ToDomain(CountryEntity e)
    {
        return Country.Create(
            CountryId.Create(e.Id),
            CountryName.Create(e.Name ?? string.Empty),
            CountryCodigoIso.Create((e.CodeIso ?? string.Empty).Trim()),
            CountryContinentId.Create(e.ContinentId)
        );
    }
}
