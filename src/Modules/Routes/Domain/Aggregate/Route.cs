using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.Aggregate;

public sealed class Route
{
    public RouteId Id { get; }
    public OriginAirportId OriginAirportId { get; }
    public DestinationAirportId DestinationAirportId { get; }
    public DistanceKm DistanceKm { get; }
    public EstimatedDurationMin EstimatedDurationMin { get; }

    private Route(RouteId id, OriginAirportId originAirportId, DestinationAirportId destinationAirportId, DistanceKm distanceKm, EstimatedDurationMin estimatedDurationMin)
    {
        Id = id;
        OriginAirportId = originAirportId;
        DestinationAirportId = destinationAirportId;
        DistanceKm = distanceKm;
        EstimatedDurationMin = estimatedDurationMin;
    }

    public static Route Create(OriginAirportId originAirportId, DestinationAirportId destinationAirportId, DistanceKm distanceKm, EstimatedDurationMin estimatedDurationMin)
    {
        return new Route(RouteId.Reconstitute(0), originAirportId, destinationAirportId, distanceKm, estimatedDurationMin);
    }

    public static Route Reconstitute(RouteId id, OriginAirportId originAirportId, DestinationAirportId destinationAirportId, DistanceKm distanceKm, EstimatedDurationMin estimatedDurationMin)
    {
        return new Route(id, originAirportId, destinationAirportId, distanceKm, estimatedDurationMin);
    }
}