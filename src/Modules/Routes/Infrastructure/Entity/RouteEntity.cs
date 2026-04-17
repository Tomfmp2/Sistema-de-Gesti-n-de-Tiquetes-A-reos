using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;

public class RouteEntity
{
    public int Id { get; set; }
    public int OriginAirportId { get; set; }
    public int DestinationAirportId { get; set; }
    public int? DistanceKm { get; set; }
    public int? EstimatedDurationMin { get; set; }

    public static RouteEntity FromDomain(Route route)
    {
        return new RouteEntity
        {
            Id = route.Id?.Value ?? 0,
            OriginAirportId = route.OriginAirportId.Value,
            DestinationAirportId = route.DestinationAirportId.Value,
            DistanceKm = route.DistanceKm.Value,
            EstimatedDurationMin = route.EstimatedDurationMin.Value
        };
    }

    public Route ToDomain()
    {
        return Route.Reconstitute(
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject.RouteId.Reconstitute(Id),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject.OriginAirportId.Reconstitute(OriginAirportId),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject.DestinationAirportId.Reconstitute(DestinationAirportId),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject.DistanceKm.Reconstitute(DistanceKm),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject.EstimatedDurationMin.Reconstitute(EstimatedDurationMin)
        );
    }
}