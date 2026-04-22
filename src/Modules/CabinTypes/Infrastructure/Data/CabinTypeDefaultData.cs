using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Data;

public static class CabinTypeDefaultData
{
    public static readonly CabinTypeEntity[] CabinTypes =
    [
        new() { Id = 1, Name = "Económica" },
        new() { Id = 2, Name = "Premium Economy" },
        new() { Id = 3, Name = "Ejecutiva" },
        new() { Id = 4, Name = "Primera clase" }
    ];
}
