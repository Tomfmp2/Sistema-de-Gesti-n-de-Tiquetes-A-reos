using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.Data;

public static class RouteLayoverDefaultData
{
    public static readonly RouteLayoverEntity[] RouteLayovers =
    [
        new() { Id = 1, RouteId = 6, LayoverAirportId = 5, SequenceOrder = 1, LayoverDurationMin = 120 },
        new() { Id = 2, RouteId = 9, LayoverAirportId = 10, SequenceOrder = 1, LayoverDurationMin = 90 },
        new() { Id = 3, RouteId = 15, LayoverAirportId = 7, SequenceOrder = 1, LayoverDurationMin = 150 }
    ];
}
