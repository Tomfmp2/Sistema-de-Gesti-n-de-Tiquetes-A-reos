using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.repository;

public class AirportRepository : IAirportRepository
{
    private readonly AppDbContext _context;

    public AirportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Airport?> GetByIdAsync(AirportId id)
    {
        var entity = await _context.Airports.FindAsync(id.Value);
        if (entity == null) return null;
        return Airport.Reconstitute(
            AirportId.Reconstitute(entity.Id),
            AirportName.Reconstitute(entity.Name),
            IataCode.Reconstitute(entity.IataCode),
            IcaoCode.Reconstitute(entity.IcaoCode),
            entity.CityId
        );
    }

    public async Task<IEnumerable<Airport>> GetAllAsync()
    {
        var entities = await _context.Airports.ToListAsync();
        return entities.Select(e => Airport.Reconstitute(
            AirportId.Reconstitute(e.Id),
            AirportName.Reconstitute(e.Name),
            IataCode.Reconstitute(e.IataCode),
            IcaoCode.Reconstitute(e.IcaoCode),
            e.CityId
        ));
    }

    public async Task AddAsync(Airport airport)
    {
        var entity = new AirportEntity
        {
            Name = airport.Name.Value,
            IataCode = airport.IataCode.Value,
            IcaoCode = airport.IcaoCode.Value,
            CityId = airport.CityId
        };
        _context.Airports.Add(entity);
        await _context.SaveChangesAsync();
        // set id back
        airport.Id = AirportId.Reconstitute(entity.Id);
    }

    public async Task UpdateAsync(Airport airport)
    {
        var entity = await _context.Airports.FindAsync(airport.Id.Value);
        if (entity == null) return;
        entity.Name = airport.Name.Value;
        entity.IataCode = airport.IataCode.Value;
        entity.IcaoCode = airport.IcaoCode.Value;
        entity.CityId = airport.CityId;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AirportId id)
    {
        var entity = await _context.Airports.FindAsync(id.Value);
        if (entity != null)
        {
            _context.Airports.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}