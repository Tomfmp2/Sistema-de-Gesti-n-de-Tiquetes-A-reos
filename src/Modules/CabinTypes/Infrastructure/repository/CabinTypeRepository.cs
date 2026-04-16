using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Aggregate;
using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.repository;

public class CabinTypeRepository : ICabinTypeRepository
{
    private readonly AppDbContext _context;

    public CabinTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Aggregate.CabinType?> GetByIdAsync(CabinTypeId id)
    {
        var entity = await _context.CabinTypes.FindAsync(id.Value);
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<Aggregate.CabinType>> GetAllAsync()
    {
        var entities = await _context.CabinTypes.ToListAsync();
        return entities.Select(e => e.ToDomain());
    }

    public async Task AddAsync(Aggregate.CabinType cabinType)
    {
        var entity = CabinTypeEntity.FromDomain(cabinType);
        await _context.CabinTypes.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Aggregate.CabinType cabinType)
    {
        var entity = CabinTypeEntity.FromDomain(cabinType);
        _context.CabinTypes.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CabinTypeId id)
    {
        var entity = await _context.CabinTypes.FindAsync(id.Value);
        if (entity != null)
        {
            _context.CabinTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}