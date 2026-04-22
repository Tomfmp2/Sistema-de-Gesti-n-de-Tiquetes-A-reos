using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Data;

public static class RegionDefaultData
{
    public static readonly RegionEntity[] Regions =
    [
        new() { Id = 1, Name = "Cundinamarca", Type = "Departamento", CountryId = 1 },
        new() { Id = 2, Name = "Antioquia", Type = "Departamento", CountryId = 1 },
        new() { Id = 3, Name = "Valle del Cauca", Type = "Departamento", CountryId = 1 },
        new() { Id = 4, Name = "Atlantico", Type = "Departamento", CountryId = 1 },
        new() { Id = 5, Name = "Florida", Type = "Estado", CountryId = 2 },
        new() { Id = 6, Name = "Nueva York", Type = "Estado", CountryId = 2 },
        new() { Id = 7, Name = "California", Type = "Estado", CountryId = 2 },
        new() { Id = 8, Name = "Ciudad de Mexico", Type = "Entidad federativa", CountryId = 3 },
        new() { Id = 9, Name = "Comunidad de Madrid", Type = "Comunidad autonoma", CountryId = 9 },
        new() { Id = 10, Name = "Ile-de-France", Type = "Region", CountryId = 10 },
        new() { Id = 11, Name = "Inglaterra", Type = "Nacion constituyente", CountryId = 11 },
        new() { Id = 12, Name = "Sao Paulo", Type = "Estado", CountryId = 4 },
        new() { Id = 13, Name = "Buenos Aires", Type = "Provincia", CountryId = 5 },
        new() { Id = 14, Name = "Region Metropolitana", Type = "Region", CountryId = 6 },
        new() { Id = 15, Name = "Lima", Type = "Departamento", CountryId = 7 },
        new() { Id = 16, Name = "Ontario", Type = "Provincia", CountryId = 8 },
        new() { Id = 17, Name = "Tokio", Type = "Prefectura", CountryId = 14 },
        new() { Id = 18, Name = "Nueva Gales del Sur", Type = "Estado", CountryId = 17 }
    ];
}
