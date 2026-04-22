using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Data;

public static class CityDefaultData
{
    public static readonly CityEntity[] Cities =
    [
        new() { Id = 1, Name = "Bogota", RegionId = 1 },
        new() { Id = 2, Name = "Medellin", RegionId = 2 },
        new() { Id = 3, Name = "Cali", RegionId = 3 },
        new() { Id = 4, Name = "Barranquilla", RegionId = 4 },
        new() { Id = 5, Name = "Miami", RegionId = 5 },
        new() { Id = 6, Name = "Nueva York", RegionId = 6 },
        new() { Id = 7, Name = "Los Angeles", RegionId = 7 },
        new() { Id = 8, Name = "Ciudad de Mexico", RegionId = 8 },
        new() { Id = 9, Name = "Madrid", RegionId = 9 },
        new() { Id = 10, Name = "Paris", RegionId = 10 },
        new() { Id = 11, Name = "Londres", RegionId = 11 },
        new() { Id = 12, Name = "Sao Paulo", RegionId = 12 },
        new() { Id = 13, Name = "Buenos Aires", RegionId = 13 },
        new() { Id = 14, Name = "Santiago", RegionId = 14 },
        new() { Id = 15, Name = "Lima", RegionId = 15 },
        new() { Id = 16, Name = "Toronto", RegionId = 16 },
        new() { Id = 17, Name = "Tokio", RegionId = 17 },
        new() { Id = 18, Name = "Sidney", RegionId = 18 }
    ];
}
