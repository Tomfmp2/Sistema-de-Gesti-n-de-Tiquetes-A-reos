using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppRoutes = sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Application.Interfaces;
using DomRoutes = sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.repository;

public class RouteRepository : DomRoutes.IRouteRepository, AppRoutes.IRouteRepository
{
    private readonly AppDbContext _context;

    public RouteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Route?> GetByIdAsync(RouteId id)
    {
        var entity = await _context.Routes.FindAsync(id.Value);
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<Route>> GetAllAsync()
    {
        var entities = await _context.Routes.ToListAsync();
        return entities.Select(e => e.ToDomain());
    }

    public async Task AddAsync(Route route)
    {
        var entity = RouteEntity.FromDomain(route);
        _context.Routes.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Route route)
    {
        var entity = RouteEntity.FromDomain(route);
        _context.Routes.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(RouteId id)
    {
        var entity = await _context.Routes.FindAsync(id.Value);
        if (entity != null)
        {
            _context.Routes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}