using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.Entity;

public class RouteLayoverEntity
{
    public int Id { get; set; }
    public int RouteId { get; set; }
    public int LayoverAirportId { get; set; }
    public int SequenceOrder { get; set; }
    public int LayoverDurationMin { get; set; }

    public static RouteLayoverEntity FromDomain(RouteLayover routeLayover)
    {
        return new RouteLayoverEntity
        {
            Id = routeLayover.Id?.Value ?? 0,
            RouteId = routeLayover.RouteId.Value,
            LayoverAirportId = routeLayover.LayoverAirportId.Value,
            SequenceOrder = routeLayover.SequenceOrder.Value,
            LayoverDurationMin = routeLayover.LayoverDurationMin.Value
        };
    }

    public RouteLayover ToDomain()
    {
        return RouteLayover.Reconstitute(
            sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject.RouteLayoverId.Reconstitute(Id),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject.RouteId.Reconstitute(RouteId),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject.LayoverAirportId.Reconstitute(LayoverAirportId),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject.SequenceOrder.Reconstitute(SequenceOrder),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject.LayoverDurationMin.Reconstitute(LayoverDurationMin)
        );
    }
}