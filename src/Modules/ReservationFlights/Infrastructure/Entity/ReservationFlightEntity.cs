namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;

public class ReservationFlightEntity
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public int FlightId { get; set; }
    public decimal PartialValue { get; set; }
}
