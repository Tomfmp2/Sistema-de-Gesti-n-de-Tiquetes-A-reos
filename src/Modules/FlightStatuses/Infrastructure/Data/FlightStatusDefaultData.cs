using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Data;

public static class FlightStatusDefaultData
{
    public static readonly FlightStatusEntity[] FlightStatuses =
    [
        new() { Id = 1, Name = "Programado" },
        new() { Id = 2, Name = "Abordando" },
        new() { Id = 3, Name = "En vuelo" },
        new() { Id = 4, Name = "Aterrizado" },
        new() { Id = 5, Name = "Retrasado" },
        new() { Id = 6, Name = "Cancelado" }
    ];
}
