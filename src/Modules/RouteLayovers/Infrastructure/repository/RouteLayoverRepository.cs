using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.repository;

public class RouteLayoverRepository : IRouteLayoverRepository
{
    private readonly AppDbContext _context;

    public RouteLayoverRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RouteLayover> GetByIdAsync(RouteLayoverId id)
    {
        var entity = await _context.RouteLayovers.FindAsync(id.Value);
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<RouteLayover>> GetAllAsync()
    {
        var entities = await _context.RouteLayovers.ToListAsync();
        return entities.Select(e => e.ToDomain());
    }

    public async Task AddAsync(RouteLayover routeLayover)
    {
        var entity = RouteLayoverEntity.FromDomain(routeLayover);
        _context.RouteLayovers.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(RouteLayover routeLayover)
    {
        var entity = RouteLayoverEntity.FromDomain(routeLayover);
        _context.RouteLayovers.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(RouteLayoverId id)
    {
        var entity = await _context.RouteLayovers.FindAsync(id.Value);
        if (entity != null)
        {
            _context.RouteLayovers.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}