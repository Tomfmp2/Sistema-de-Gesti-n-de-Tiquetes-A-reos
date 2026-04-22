using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Data;

public static class ContinentDefaultData
{
    public static readonly ContinentEntity[] Continents =
    [
        new() { Id = 1, Name = "América" },
        new() { Id = 2, Name = "Europa" },
        new() { Id = 3, Name = "Asia" },
        new() { Id = 4, Name = "África" },
        new() { Id = 5, Name = "Oceanía" },
        new() { Id = 6, Name = "Antártida" }
    ];
}
