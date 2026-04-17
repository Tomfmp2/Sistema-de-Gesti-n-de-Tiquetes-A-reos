using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.repository;

public sealed class StreetTypeRepository : IStreetTypeRepository
{
    private readonly AppDbContext _context;

    public StreetTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StreetType?> GetByIdAsync(
        StreetTypeId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<StreetTypeEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<StreetType>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<StreetTypeEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<StreetType> AddAsync(StreetType entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use StreetType.CreateNew para insertar.");
        }

        var e = new StreetTypeEntity { Name = entity.Name.Value };
        _context.Set<StreetTypeEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(StreetType entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<StreetTypeEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe tipo de vía {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(StreetTypeId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<StreetTypeEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<StreetTypeEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static StreetType ToDomain(StreetTypeEntity e)
    {
        return StreetType.Create(
            StreetTypeId.Create(e.Id),
            StreetTypeName.Create(e.Name ?? string.Empty)
        );
    }
}
