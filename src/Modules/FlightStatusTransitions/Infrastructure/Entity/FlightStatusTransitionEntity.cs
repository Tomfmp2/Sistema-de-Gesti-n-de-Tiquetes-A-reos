using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Infrastructure.Entity;

public class FlightStatusTransitionEntity
{
    public int Id { get; set; }
    public int OriginStatusId { get; set; }
    public int DestinationStatusId { get; set; }

    public FlightStatusEntity? OriginStatus { get; set; }
    public FlightStatusEntity? DestinationStatus { get; set; }
}
