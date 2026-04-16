using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Application.UseCases;

public class CreateAircraftModelUseCase
{
    private readonly IAircraftModelRepository _repository;

    public CreateAircraftModelUseCase(IAircraftModelRepository repository)
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
        var aircraftModel = AircraftModel.Create(id, manufacturerId, modelName, maxCapacity, maxTakeoffWeightKg, fuelConsumptionKgH, cruisingSpeedKmh, cruisingAltitudeFt);
        await _repository.AddAsync(aircraftModel);
    }
}