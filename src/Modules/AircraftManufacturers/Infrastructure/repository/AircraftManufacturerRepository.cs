using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.repository;

public class AircraftManufacturerRepository : IAircraftManufacturerRepository
{
    private readonly AppDbContext _context;

    public AircraftManufacturerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AircraftManufacturer?> GetByIdAsync(AircraftManufacturerId id)
    {
        var entity = await _context.AircraftManufacturers.FindAsync(id.Value);
        if (entity == null) return null;

        return AircraftManufacturer.Reconstitute(
            AircraftManufacturerId.Reconstitute(entity.Id),
            AircraftManufacturerName.Reconstitute(entity.Name),
            Country.Reconstitute(entity.Country)
        );
    }

    public async Task<IEnumerable<AircraftManufacturer>> GetAllAsync()
    {
        var entities = await _context.AircraftManufacturers.ToListAsync();
        return entities.Select(entity => AircraftManufacturer.Reconstitute(
            AircraftManufacturerId.Reconstitute(entity.Id),
            AircraftManufacturerName.Reconstitute(entity.Name),
            Country.Reconstitute(entity.Country)
        ));
    }

    public async Task AddAsync(AircraftManufacturer aircraftManufacturer)
    {
        var entity = new AircraftManufacturerEntity
        {
            Id = aircraftManufacturer.Id.Value,
            Name = aircraftManufacturer.Name.Value,
            Country = aircraftManufacturer.Country.Value
        };

        await _context.AircraftManufacturers.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AircraftManufacturer aircraftManufacturer)
    {
        var entity = await _context.AircraftManufacturers.FindAsync(aircraftManufacturer.Id.Value);
        if (entity == null) return;

        entity.Name = aircraftManufacturer.Name.Value;
        entity.Country = aircraftManufacturer.Country.Value;

        _context.AircraftManufacturers.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AircraftManufacturerId id)
    {
        var entity = await _context.AircraftManufacturers.FindAsync(id.Value);
        if (entity == null) return;

        _context.AircraftManufacturers.Remove(entity);
        await _context.SaveChangesAsync();
    }
}