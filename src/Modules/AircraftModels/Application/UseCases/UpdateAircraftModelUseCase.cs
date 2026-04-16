using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Application.UseCases;

public class UpdateAircraftModelUseCase
{
    private readonly IAircraftModelRepository _repository;

    public UpdateAircraftModelUseCase(IAircraftModelRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        AircraftModelId id,
        ManufacturerId manufacturerId,
        ModelName modelName,
        MaxCapacity maxCapacity,
        MaxTakeoffWeightKg? maxTakeoffWeightKg,
        FuelConsumptionKgH? fuelConsumptionKgH,
        CruisingSpeedKmh? cruisingSpeedKmh,
        CruisingAltitudeFt? cruisingAltitudeFt)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            throw new InvalidOperationException("Aircraft model not found.");
        }

        var updated = AircraftModel.Create(id, manufacturerId, modelName, maxCapacity, maxTakeoffWeightKg, fuelConsumptionKgH, cruisingSpeedKmh, cruisingAltitudeFt);
        await _repository.UpdateAsync(updated);
    }
}