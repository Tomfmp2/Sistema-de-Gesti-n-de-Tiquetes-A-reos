using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.repository;

public class FlightStatusRepository : IFlightStatusRepository
{
    private readonly AppDbContext _context;

    public FlightStatusRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FlightStatus> GetByIdAsync(FlightStatusId id)
    {
        var entity = await _context.FlightStatuses.FindAsync(id.Value);
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<FlightStatus>> GetAllAsync()
    {
        var entities = await _context.FlightStatuses.ToListAsync();
        return entities.Select(e => e.ToDomain());
    }

    public async Task AddAsync(FlightStatus flightStatus)
    {
        var entity = FlightStatusEntity.FromDomain(flightStatus);
        _context.FlightStatuses.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(FlightStatus flightStatus)
    {
        var entity = FlightStatusEntity.FromDomain(flightStatus);
        _context.FlightStatuses.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(FlightStatusId id)
    {
        var entity = await _context.FlightStatuses.FindAsync(id.Value);
        if (entity != null)
        {
            _context.FlightStatuses.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}