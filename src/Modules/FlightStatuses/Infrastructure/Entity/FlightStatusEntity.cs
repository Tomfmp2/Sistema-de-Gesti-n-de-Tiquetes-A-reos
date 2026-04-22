using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;

public class FlightStatusEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<FlightEntity> Flights { get; set; } = new List<FlightEntity>();
    public ICollection<FlightStatusTransitionEntity> OriginTransitions { get; set; } =
        new List<FlightStatusTransitionEntity>();
    public ICollection<FlightStatusTransitionEntity> DestinationTransitions { get; set; } =
        new List<FlightStatusTransitionEntity>();
}
