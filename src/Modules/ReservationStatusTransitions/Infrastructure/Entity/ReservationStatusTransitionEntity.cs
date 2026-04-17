namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Infrastructure.Entity;

public class ReservationStatusTransitionEntity
{
    public int Id { get; set; }
    public int OriginStatusId { get; set; }
    public int DestinationStatusId { get; set; }
}
