using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Infrastructure.Data;

public static class FlightStatusTransitionDefaultData
{
    public static readonly FlightStatusTransitionEntity[] FlightStatusTransitions =
    [
        new() { Id = 1, OriginStatusId = 1, DestinationStatusId = 2 },
        new() { Id = 2, OriginStatusId = 2, DestinationStatusId = 3 },
        new() { Id = 3, OriginStatusId = 3, DestinationStatusId = 4 },
        new() { Id = 4, OriginStatusId = 1, DestinationStatusId = 5 },
        new() { Id = 5, OriginStatusId = 5, DestinationStatusId = 1 },
        new() { Id = 6, OriginStatusId = 1, DestinationStatusId = 6 },
        new() { Id = 7, OriginStatusId = 5, DestinationStatusId = 6 }
    ];
}
