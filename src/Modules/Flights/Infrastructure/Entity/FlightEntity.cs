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
}
