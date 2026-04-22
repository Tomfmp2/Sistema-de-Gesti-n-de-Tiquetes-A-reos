using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Data;

public static class BaggageTypeDefaultData
{
    public static readonly BaggageTypeEntity[] BaggageTypes =
    [
        new() { Id = 1, Name = "Artículo personal", MaxWeightKg = 5.00m, BasePrice = 0.00m },
        new() { Id = 2, Name = "Equipaje de mano", MaxWeightKg = 10.00m, BasePrice = 0.00m },
        new() { Id = 3, Name = "Bodega estándar", MaxWeightKg = 23.00m, BasePrice = 60000.00m },
        new() { Id = 4, Name = "Bodega extra", MaxWeightKg = 32.00m, BasePrice = 120000.00m }
    ];
}
