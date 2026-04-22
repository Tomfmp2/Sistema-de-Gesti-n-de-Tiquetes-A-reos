using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Data;

public static class StreetTypeDefaultData
{
    public static readonly StreetTypeEntity[] StreetTypes =
    [
        new() { Id = 1, Name = "Calle" },
        new() { Id = 2, Name = "Carrera" },
        new() { Id = 3, Name = "Avenida" },
        new() { Id = 4, Name = "Diagonal" },
        new() { Id = 5, Name = "Transversal" },
        new() { Id = 6, Name = "Autopista" }
    ];
}
