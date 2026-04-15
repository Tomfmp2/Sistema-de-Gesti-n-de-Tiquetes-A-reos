namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;

public class ReservationPassengerEntity
{
    public int Id { get; set; }
    public int ReservationFlightId { get; set; }
    public int PassengerId { get; set; }
}
