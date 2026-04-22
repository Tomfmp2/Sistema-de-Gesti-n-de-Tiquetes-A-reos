using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Data;

public static class AircraftModelDefaultData
{
    public static readonly AircraftModelEntity[] AircraftModels =
    [
        new()
        {
            Id = 1,
            ManufacturerId = 1,
            ModelName = "Airbus A320",
            MaxCapacity = 180,
            MaxTakeoffWeightKg = 78000.00m,
            FuelConsumptionKgH = 2500.00m,
            CruisingSpeedKmh = 840,
            CruisingAltitudeFt = 39000
        },
        new()
        {
            Id = 2,
            ManufacturerId = 1,
            ModelName = "Airbus A330",
            MaxCapacity = 277,
            MaxTakeoffWeightKg = 242000.00m,
            FuelConsumptionKgH = 5500.00m,
            CruisingSpeedKmh = 871,
            CruisingAltitudeFt = 41000
        },
        new()
        {
            Id = 3,
            ManufacturerId = 2,
            ModelName = "Boeing 737-800",
            MaxCapacity = 189,
            MaxTakeoffWeightKg = 79015.00m,
            FuelConsumptionKgH = 2600.00m,
            CruisingSpeedKmh = 842,
            CruisingAltitudeFt = 41000
        },
        new()
        {
            Id = 4,
            ManufacturerId = 2,
            ModelName = "Boeing 787-8",
            MaxCapacity = 242,
            MaxTakeoffWeightKg = 227930.00m,
            FuelConsumptionKgH = 4900.00m,
            CruisingSpeedKmh = 903,
            CruisingAltitudeFt = 43000
        },
        new()
        {
            Id = 5,
            ManufacturerId = 3,
            ModelName = "Embraer E190",
            MaxCapacity = 114,
            MaxTakeoffWeightKg = 51800.00m,
            FuelConsumptionKgH = 1500.00m,
            CruisingSpeedKmh = 829,
            CruisingAltitudeFt = 41000
        }
    ];
}
