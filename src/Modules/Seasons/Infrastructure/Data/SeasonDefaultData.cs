using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Data;

public static class SeasonDefaultData
{
    public static readonly SeasonEntity[] Seasons =
    [
        new() { Id = 1, Name = "Baja", Description = "Temporada de menor demanda", PriceFactor = 0.9000m },
        new() { Id = 2, Name = "Media", Description = "Temporada regular", PriceFactor = 1.0000m },
        new() { Id = 3, Name = "Alta", Description = "Temporada de alta demanda", PriceFactor = 1.2500m },
        new() { Id = 4, Name = "Festiva", Description = "Temporada de festivos y vacaciones", PriceFactor = 1.4000m }
    ];
}
