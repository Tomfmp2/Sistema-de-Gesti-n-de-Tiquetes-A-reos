using System.Linq;
using Microsoft.EntityFrameworkCore;
using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.repository;

public class CabinConfigurationRepository : ICabinConfigurationRepository
{
    private readonly AppDbContext _context;

    public CabinConfigurationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Aggregate.CabinConfiguration?> GetByIdAsync(CabinConfigurationId id)
    {
        var entity = await _context.CabinConfiguration.FindAsync(id.Value);
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<Aggregate.CabinConfiguration>> GetAllAsync()
    {
        var entities = await _context.CabinConfiguration.ToListAsync();
        return entities.Select(e => e.ToDomain());
    }

    public async Task AddAsync(Aggregate.CabinConfiguration cabinConfiguration)
    {
        var entity = CabinConfigurationEntity.FromDomain(cabinConfiguration);
        await _context.CabinConfiguration.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Aggregate.CabinConfiguration cabinConfiguration)
    {
        var entity = CabinConfigurationEntity.FromDomain(cabinConfiguration);
        _context.CabinConfiguration.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CabinConfigurationId id)
    {
        var entity = await _context.CabinConfiguration.FindAsync(id.Value);
        if (entity != null)
        {
            _context.CabinConfiguration.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}