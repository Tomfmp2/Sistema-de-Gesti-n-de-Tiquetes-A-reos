using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;

public class FlightEntity
{
    public int Id { get; set; }
    public string? FlightCode { get; set; }
    public int AirlineId { get; set; }
    public int RouteId { get; set; }
    public int AircraftId { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime EstimatedArrivalDate { get; set; }
    public int TotalCapacity { get; set; }
    public int AvailableSeats { get; set; }
    public int FlightStatusId { get; set; }
    public DateTime? RescheduledAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public AirlineEntity? Airline { get; set; }
    public RouteEntity? Route { get; set; }
    public AircraftEntity? Aircraft { get; set; }
    public FlightStatusEntity? FlightStatus { get; set; }
    public ICollection<FlightAssignmentEntity> FlightAssignments { get; set; } = new List<FlightAssignmentEntity>();
    public ICollection<FlightSeatEntity> FlightSeats { get; set; } = new List<FlightSeatEntity>();
    public ICollection<ReservationFlightEntity> ReservationFlights { get; set; } = new List<ReservationFlightEntity>();
}
