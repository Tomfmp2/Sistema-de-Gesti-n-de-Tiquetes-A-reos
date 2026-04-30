using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Data;

public static class AircraftDefaultData
{
    public static readonly AircraftEntity[] Aircraft =
    [
        new() { Id = 1, ModelId = 1, AirlineId = 1, Registration = "HK-5310", ManufacturingDate = new DateOnly(2018, 5, 15), IsActive = true },
        new() { Id = 2, ModelId = 1, AirlineId = 1, Registration = "HK-5321", ManufacturingDate = new DateOnly(2019, 8, 20), IsActive = true },
        new() { Id = 3, ModelId = 3, AirlineId = 2, Registration = "CC-BGA", ManufacturingDate = new DateOnly(2017, 3, 10), IsActive = true },
        new() { Id = 4, ModelId = 4, AirlineId = 3, Registration = "N801AA", ManufacturingDate = new DateOnly(2020, 2, 5), IsActive = true },
        new() { Id = 5, ModelId = 2, AirlineId = 4, Registration = "EC-MKI", ManufacturingDate = new DateOnly(2016, 11, 18), IsActive = true },
        new() { Id = 6, ModelId = 2, AirlineId = 5, Registration = "F-GZCA", ManufacturingDate = new DateOnly(2015, 6, 25), IsActive = true },
        // Demo: 3 cabinas (≈100 turista / 20 business / 10 first) — ver cabin_configurations
        new() { Id = 7, ModelId = 4, AirlineId = 1, Registration = "DEMO-777W", ManufacturingDate = new DateOnly(2021, 3, 1), IsActive = true }
    ];
}
