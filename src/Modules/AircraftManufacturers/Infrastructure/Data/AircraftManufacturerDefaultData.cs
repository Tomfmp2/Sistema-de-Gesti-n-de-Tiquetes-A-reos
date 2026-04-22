using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Data;

public static class AircraftManufacturerDefaultData
{
    public static readonly AircraftManufacturerEntity[] AircraftManufacturers =
    [
        new() { Id = 1, Name = "Airbus", Country = "Francia" },
        new() { Id = 2, Name = "Boeing", Country = "Estados Unidos" },
        new() { Id = 3, Name = "Embraer", Country = "Brasil" },
        new() { Id = 4, Name = "ATR", Country = "Francia" },
        new() { Id = 5, Name = "De Havilland Canada", Country = "Canadá" }
    ];
}
