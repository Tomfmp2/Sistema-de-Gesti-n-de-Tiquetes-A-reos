using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Data;

public static class RouteDefaultData
{
    public static readonly RouteEntity[] Routes =
    [
        new() { Id = 1, OriginAirportId = 1, DestinationAirportId = 2, DistanceKm = 215, EstimatedDurationMin = 55 },
        new() { Id = 2, OriginAirportId = 1, DestinationAirportId = 3, DistanceKm = 280, EstimatedDurationMin = 60 },
        new() { Id = 3, OriginAirportId = 1, DestinationAirportId = 4, DistanceKm = 695, EstimatedDurationMin = 95 },
        new() { Id = 4, OriginAirportId = 1, DestinationAirportId = 5, DistanceKm = 2435, EstimatedDurationMin = 225 },
        new() { Id = 5, OriginAirportId = 1, DestinationAirportId = 8, DistanceKm = 3160, EstimatedDurationMin = 270 },
        new() { Id = 6, OriginAirportId = 1, DestinationAirportId = 9, DistanceKm = 8030, EstimatedDurationMin = 620 },
        new() { Id = 7, OriginAirportId = 1, DestinationAirportId = 15, DistanceKm = 1880, EstimatedDurationMin = 185 },
        new() { Id = 8, OriginAirportId = 5, DestinationAirportId = 6, DistanceKm = 1760, EstimatedDurationMin = 180 },
        new() { Id = 9, OriginAirportId = 6, DestinationAirportId = 11, DistanceKm = 5540, EstimatedDurationMin = 415 },
        new() { Id = 10, OriginAirportId = 9, DestinationAirportId = 10, DistanceKm = 1060, EstimatedDurationMin = 125 },
        new() { Id = 11, OriginAirportId = 9, DestinationAirportId = 11, DistanceKm = 1245, EstimatedDurationMin = 145 },
        new() { Id = 12, OriginAirportId = 12, DestinationAirportId = 13, DistanceKm = 1720, EstimatedDurationMin = 175 },
        new() { Id = 13, OriginAirportId = 13, DestinationAirportId = 14, DistanceKm = 1140, EstimatedDurationMin = 125 },
        new() { Id = 14, OriginAirportId = 14, DestinationAirportId = 15, DistanceKm = 2460, EstimatedDurationMin = 220 },
        new() { Id = 15, OriginAirportId = 17, DestinationAirportId = 18, DistanceKm = 7800, EstimatedDurationMin = 575 }
    ];
}
