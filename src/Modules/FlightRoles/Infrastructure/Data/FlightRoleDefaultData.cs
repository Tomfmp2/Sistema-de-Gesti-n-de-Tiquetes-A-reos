using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Data;

public static class FlightRoleDefaultData
{
    public static readonly FlightRoleEntity[] FlightRoles =
    [
        new() { Id = 1, Name = "Capitán" },
        new() { Id = 2, Name = "Primer oficial" },
        new() { Id = 3, Name = "Jefe de cabina" },
        new() { Id = 4, Name = "Auxiliar de vuelo" }
    ];
}
