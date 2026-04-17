namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Infrastructure.Entity;

public class FlightStatusTransitionEntity
{
    public int Id { get; set; }
    public int OriginStatusId { get; set; }
    public int DestinationStatusId { get; set; }
}
