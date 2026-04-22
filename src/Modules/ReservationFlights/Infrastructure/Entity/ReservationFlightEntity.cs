using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;

public class ReservationFlightEntity
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public int FlightId { get; set; }
    public decimal PartialValue { get; set; }

    // Navigation properties
    public ReservationEntity? Reservation { get; set; }
    public FlightEntity? Flight { get; set; }
    public ICollection<ReservationPassengerEntity> ReservationPassengers { get; set; } =
        new List<ReservationPassengerEntity>();
}
