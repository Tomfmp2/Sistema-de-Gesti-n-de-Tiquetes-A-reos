using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Aggregate;

public class AircraftModel
{
    public AircraftModelId Id { get; private set; }
    public ManufacturerId ManufacturerId { get; private set; }
    public ModelName ModelName { get; private set; }
    public MaxCapacity MaxCapacity { get; private set; }
    public MaxTakeoffWeightKg? MaxTakeoffWeightKg { get; private set; }
    public FuelConsumptionKgH? FuelConsumptionKgH { get; private set; }
    public CruisingSpeedKmh? CruisingSpeedKmh { get; private set; }
    public CruisingAltitudeFt? CruisingAltitudeFt { get; private set; }

    private AircraftModel(
        AircraftModelId id,
        ManufacturerId manufacturerId,
        ModelName modelName,
        MaxCapacity maxCapacity,
        MaxTakeoffWeightKg? maxTakeoffWeightKg,
        FuelConsumptionKgH? fuelConsumptionKgH,
        CruisingSpeedKmh? cruisingSpeedKmh,
        CruisingAltitudeFt? cruisingAltitudeFt)
    {
        Id = id;
        ManufacturerId = manufacturerId;
        ModelName = modelName;
        MaxCapacity = maxCapacity;
        MaxTakeoffWeightKg = maxTakeoffWeightKg;
        FuelConsumptionKgH = fuelConsumptionKgH;
        CruisingSpeedKmh = cruisingSpeedKmh;
        CruisingAltitudeFt = cruisingAltitudeFt;
    }

    public static AircraftModel Create(
        AircraftModelId id,
        ManufacturerId manufacturerId,
        ModelName modelName,
        MaxCapacity maxCapacity,
        MaxTakeoffWeightKg? maxTakeoffWeightKg,
        FuelConsumptionKgH? fuelConsumptionKgH,
        CruisingSpeedKmh? cruisingSpeedKmh,
        CruisingAltitudeFt? cruisingAltitudeFt)
    {
        return new AircraftModel(id, manufacturerId, modelName, maxCapacity, maxTakeoffWeightKg, fuelConsumptionKgH, cruisingSpeedKmh, cruisingAltitudeFt);
    }

    public static AircraftModel Reconstitute(
        AircraftModelId id,
        ManufacturerId manufacturerId,
        ModelName modelName,
        MaxCapacity maxCapacity,
        MaxTakeoffWeightKg? maxTakeoffWeightKg,
        FuelConsumptionKgH? fuelConsumptionKgH,
        CruisingSpeedKmh? cruisingSpeedKmh,
        CruisingAltitudeFt? cruisingAltitudeFt)
    {
        return new AircraftModel(id, manufacturerId, modelName, maxCapacity, maxTakeoffWeightKg, fuelConsumptionKgH, cruisingSpeedKmh, cruisingAltitudeFt);
    }
}