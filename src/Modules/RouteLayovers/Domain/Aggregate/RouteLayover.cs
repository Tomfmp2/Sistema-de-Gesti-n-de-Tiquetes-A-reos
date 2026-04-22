using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.Aggregate;

public sealed class RouteLayover
{
    public RouteLayoverId Id { get; }
    public RouteId RouteId { get; }
    public LayoverAirportId LayoverAirportId { get; }
    public SequenceOrder SequenceOrder { get; }
    public LayoverDurationMin LayoverDurationMin { get; }

    private RouteLayover(RouteLayoverId id, RouteId routeId, LayoverAirportId layoverAirportId, SequenceOrder sequenceOrder, LayoverDurationMin layoverDurationMin)
    {
        Id = id;
        RouteId = routeId;
        LayoverAirportId = layoverAirportId;
        SequenceOrder = sequenceOrder;
        LayoverDurationMin = layoverDurationMin;
    }

    public static RouteLayover Create(RouteId routeId, LayoverAirportId layoverAirportId, SequenceOrder sequenceOrder, LayoverDurationMin layoverDurationMin)
    {
        return new RouteLayover(RouteLayoverId.Reconstitute(0), routeId, layoverAirportId, sequenceOrder, layoverDurationMin);
    }

    public static RouteLayover Reconstitute(RouteLayoverId id, RouteId routeId, LayoverAirportId layoverAirportId, SequenceOrder sequenceOrder, LayoverDurationMin layoverDurationMin)
    {
        return new RouteLayover(id, routeId, layoverAirportId, sequenceOrder, layoverDurationMin);
    }
}