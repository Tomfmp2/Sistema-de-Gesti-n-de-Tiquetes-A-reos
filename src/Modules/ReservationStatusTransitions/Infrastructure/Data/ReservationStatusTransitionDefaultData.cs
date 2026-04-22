using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Infrastructure.Data;

public static class ReservationStatusTransitionDefaultData
{
    public static readonly ReservationStatusTransitionEntity[] ReservationStatusTransitions =
    [
        new() { Id = 1, OriginStatusId = 1, DestinationStatusId = 2 },
        new() { Id = 2, OriginStatusId = 1, DestinationStatusId = 3 },
        new() { Id = 3, OriginStatusId = 1, DestinationStatusId = 4 },
        new() { Id = 4, OriginStatusId = 2, DestinationStatusId = 3 },
        new() { Id = 5, OriginStatusId = 2, DestinationStatusId = 5 }
    ];
}
