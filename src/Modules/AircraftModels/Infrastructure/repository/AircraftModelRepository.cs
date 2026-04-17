using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.repository;

public class AircraftModelRepository : IAircraftModelRepository
{
    private readonly AppDbContext _context;

    public AircraftModelRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AircraftModel?> GetByIdAsync(AircraftModelId id)
    {
        var entity = await _context.AircraftModels.FindAsync(id.Value);
        if (entity == null) return null;

        return AircraftModel.Reconstitute(
            AircraftModelId.Reconstitute(entity.Id),
            ManufacturerId.Reconstitute(entity.ManufacturerId),
            ModelName.Reconstitute(entity.ModelName),
            MaxCapacity.Reconstitute(entity.MaxCapacity),
            entity.MaxTakeoffWeightKg.HasValue ? MaxTakeoffWeightKg.Reconstitute(entity.MaxTakeoffWeightKg.Value) : null,
            entity.FuelConsumptionKgH.HasValue ? FuelConsumptionKgH.Reconstitute(entity.FuelConsumptionKgH.Value) : null,
            entity.CruisingSpeedKmh.HasValue ? CruisingSpeedKmh.Reconstitute(entity.CruisingSpeedKmh.Value) : null,
            entity.CruisingAltitudeFt.HasValue ? CruisingAltitudeFt.Reconstitute(entity.CruisingAltitudeFt.Value) : null
        );
    }

    public async Task<IEnumerable<AircraftModel>> GetAllAsync()
    {
        var entities = await _context.AircraftModels.ToListAsync();
        return entities.Select(entity => AircraftModel.Reconstitute(
            AircraftModelId.Reconstitute(entity.Id),
            ManufacturerId.Reconstitute(entity.ManufacturerId),
            ModelName.Reconstitute(entity.ModelName),
            MaxCapacity.Reconstitute(entity.MaxCapacity),
            entity.MaxTakeoffWeightKg.HasValue ? MaxTakeoffWeightKg.Reconstitute(entity.MaxTakeoffWeightKg.Value) : null,
            entity.FuelConsumptionKgH.HasValue ? FuelConsumptionKgH.Reconstitute(entity.FuelConsumptionKgH.Value) : null,
            entity.CruisingSpeedKmh.HasValue ? CruisingSpeedKmh.Reconstitute(entity.CruisingSpeedKmh.Value) : null,
            entity.CruisingAltitudeFt.HasValue ? CruisingAltitudeFt.Reconstitute(entity.CruisingAltitudeFt.Value) : null
        ));
    }

    public async Task AddAsync(AircraftModel aircraftModel)
    {
        var entity = new AircraftModelEntity
        {
            Id = aircraftModel.Id.Value,
            ManufacturerId = aircraftModel.ManufacturerId.Value,
            ModelName = aircraftModel.ModelName.Value,
            MaxCapacity = aircraftModel.MaxCapacity.Value,
            MaxTakeoffWeightKg = aircraftModel.MaxTakeoffWeightKg?.Value,
            FuelConsumptionKgH = aircraftModel.FuelConsumptionKgH?.Value,
            CruisingSpeedKmh = aircraftModel.CruisingSpeedKmh?.Value,
            CruisingAltitudeFt = aircraftModel.CruisingAltitudeFt?.Value
        };

        await _context.AircraftModels.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AircraftModel aircraftModel)
    {
        var entity = await _context.AircraftModels.FindAsync(aircraftModel.Id.Value);
        if (entity == null) return;

        entity.ManufacturerId = aircraftModel.ManufacturerId.Value;
        entity.ModelName = aircraftModel.ModelName.Value;
        entity.MaxCapacity = aircraftModel.MaxCapacity.Value;
        entity.MaxTakeoffWeightKg = aircraftModel.MaxTakeoffWeightKg?.Value;
        entity.FuelConsumptionKgH = aircraftModel.FuelConsumptionKgH?.Value;
        entity.CruisingSpeedKmh = aircraftModel.CruisingSpeedKmh?.Value;
        entity.CruisingAltitudeFt = aircraftModel.CruisingAltitudeFt?.Value;

        _context.AircraftModels.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AircraftModelId id)
    {
        var entity = await _context.AircraftModels.FindAsync(id.Value);
        if (entity == null) return;

        _context.AircraftModels.Remove(entity);
        await _context.SaveChangesAsync();
    }
}