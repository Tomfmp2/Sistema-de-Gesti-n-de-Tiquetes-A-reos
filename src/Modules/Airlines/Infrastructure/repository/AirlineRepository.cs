using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.repository;

public class AirlineRepository : IAirlineRepository
{
    private readonly AppDbContext _context;

    public AirlineRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Airline?> GetByIdAsync(AirlineId id)
    {
        var entity = await _context.Airlines.FindAsync(id.Value);
        if (entity == null) return null;
        return Airline.Reconstitute(
            AirlineId.Reconstitute(entity.Id),
            AirlineName.Reconstitute(entity.Name),
            IataCode.Reconstitute(entity.IataCode),
            entity.OriginCountryId,
            entity.IsActive,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }

    public async Task<IEnumerable<Airline>> GetAllAsync()
    {
        var entities = await _context.Airlines.ToListAsync();
        return entities.Select(e => Airline.Reconstitute(
            AirlineId.Reconstitute(e.Id),
            AirlineName.Reconstitute(e.Name),
            IataCode.Reconstitute(e.IataCode),
            e.OriginCountryId,
            e.IsActive,
            e.CreatedAt,
            e.UpdatedAt
        ));
    }

    public async Task AddAsync(Airline airline)
    {
        var entity = new AirlineEntity
        {
            Name = airline.Name.Value,
            IataCode = airline.IataCode.Value,
            OriginCountryId = airline.OriginCountryId,
            IsActive = airline.IsActive,
            CreatedAt = airline.CreatedAt,
            UpdatedAt = airline.UpdatedAt
        };
        _context.Airlines.Add(entity);
        await _context.SaveChangesAsync();
        // set id back
        airline.Id = AirlineId.Reconstitute(entity.Id);
    }

    public async Task UpdateAsync(Airline airline)
    {
        var entity = await _context.Airlines.FindAsync(airline.Id.Value);
        if (entity == null) return;
        entity.Name = airline.Name.Value;
        entity.IataCode = airline.IataCode.Value;
        entity.OriginCountryId = airline.OriginCountryId;
        entity.IsActive = airline.IsActive;
        entity.UpdatedAt = airline.UpdatedAt;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AirlineId id)
    {
        var entity = await _context.Airlines.FindAsync(id.Value);
        if (entity != null)
        {
            _context.Airlines.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}