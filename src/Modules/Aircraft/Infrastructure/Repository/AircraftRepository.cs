using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Aggregate;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Repository;

public class AircraftRepository : IAircraftRepository
{
    private readonly AppDbContext _context;

    public AircraftRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Aggregate.Aircraft?> GetByIdAsync(AircraftId id)
    {
        var entity = await _context.Aircraft.FindAsync(id.Value);
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<Aggregate.Aircraft>> GetAllAsync()
    {
        var entities = await _context.Aircraft.ToListAsync();
        return entities.Select(e => e.ToDomain());
    }

    public async Task AddAsync(Aggregate.Aircraft aircraft)
    {
        var entity = AircraftEntity.FromDomain(aircraft);
        await _context.Aircraft.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Aggregate.Aircraft aircraft)
    {
        var entity = AircraftEntity.FromDomain(aircraft);
        _context.Aircraft.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AircraftId id)
    {
        var entity = await _context.Aircraft.FindAsync(id.Value);
        if (entity != null)
        {
            _context.Aircraft.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}