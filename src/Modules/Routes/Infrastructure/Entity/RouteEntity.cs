using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;

public class RouteEntity
{
    public int Id { get; set; }
    public int OriginAirportId { get; set; }
    public int DestinationAirportId { get; set; }
    public int? DistanceKm { get; set; }
    public int? EstimatedDurationMin { get; set; }
    public decimal Miles { get; set; }

    // Navigation properties
    public AirportEntity? OriginAirport { get; set; }
    public AirportEntity? DestinationAirport { get; set; }
    public ICollection<FlightEntity> Flights { get; set; } = new List<FlightEntity>();
    public ICollection<RouteLayoverEntity> RouteLayovers { get; set; } = new List<RouteLayoverEntity>();
    public ICollection<FareEntity> Fares { get; set; } = new List<FareEntity>();

    public static RouteEntity FromDomain(Route route)
    {
        return new RouteEntity
        {
            Id = route.Id?.Value ?? 0,
            OriginAirportId = route.OriginAirportId.Value,
            DestinationAirportId = route.DestinationAirportId.Value,
            DistanceKm = route.DistanceKm.Value,
            EstimatedDurationMin = route.EstimatedDurationMin.Value,
            Miles = route.Miles.Value
        };
    }

    public Route ToDomain()
    {
        return Route.Reconstitute(
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject.RouteId.Reconstitute(Id),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject.OriginAirportId.Reconstitute(OriginAirportId),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject.DestinationAirportId.Reconstitute(DestinationAirportId),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject.DistanceKm.Reconstitute(DistanceKm),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject.EstimatedDurationMin.Reconstitute(EstimatedDurationMin),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject.RouteMiles.Reconstitute(Miles)
        );
    }
}