using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.repository;

public class AirportAirlineRepository : IAirportAirlineRepository
{
    private readonly AppDbContext _context;

    public AirportAirlineRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AirportAirlineRecord?> GetByIdAsync(AirportAirlineId id)
    {
        var entity = await _context.AirportAirlines.FindAsync(id.Value);
        if (entity == null) return null;
        return AirportAirlineRecord.Reconstitute(
            AirportAirlineId.Reconstitute(entity.Id),
            entity.AirportId,
            entity.AirlineId,
            Terminal.Reconstitute(entity.Terminal),
            entity.StartDate,
            entity.EndDate,
            entity.IsActive
        );
    }

    public async Task<IEnumerable<AirportAirlineRecord>> GetAllAsync()
    {
        var entities = await _context.AirportAirlines.Where(x => x.IsActive).ToListAsync();
        return entities.Select(e => AirportAirlineRecord.Reconstitute(
            AirportAirlineId.Reconstitute(e.Id),
            e.AirportId,
            e.AirlineId,
            Terminal.Reconstitute(e.Terminal),
            e.StartDate,
            e.EndDate,
            e.IsActive
        ));
    }

    public async Task AddAsync(AirportAirlineRecord airportAirline)
    {
        var entity = new AirportAirlineEntity
        {
            AirportId = airportAirline.AirportId,
            AirlineId = airportAirline.AirlineId,
            Terminal = airportAirline.Terminal.Value,
            StartDate = airportAirline.StartDate,
            EndDate = airportAirline.EndDate,
            IsActive = airportAirline.IsActive
        };
        _context.AirportAirlines.Add(entity);
        await _context.SaveChangesAsync();
        // set id back
        airportAirline.Id = AirportAirlineId.Reconstitute(entity.Id);
    }

    public async Task UpdateAsync(AirportAirlineRecord airportAirline)
    {
        var entity = await _context.AirportAirlines.FindAsync(airportAirline.Id.Value);
        if (entity == null) return;
        entity.AirportId = airportAirline.AirportId;
        entity.AirlineId = airportAirline.AirlineId;
        entity.Terminal = airportAirline.Terminal.Value;
        entity.StartDate = airportAirline.StartDate;
        entity.EndDate = airportAirline.EndDate;
        entity.IsActive = airportAirline.IsActive;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AirportAirlineId id)
    {
        var entity = await _context.AirportAirlines.FindAsync(id.Value);
        if (entity != null)
        {
            entity.IsActive = false;
            await _context.SaveChangesAsync();
        }
    }
}