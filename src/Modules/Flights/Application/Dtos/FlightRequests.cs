namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.Dtos;

public sealed record CreateFlightRequest(string? FlightCode, int AirlineId, int RouteId, int AircraftId, DateTime DepartureDate, DateTime EstimatedArrivalDate, int TotalCapacity, int AvailableSeats, int FlightStatusId, DateTime? RescheduledAt, DateTime CreatedAt, DateTime UpdatedAt);

public sealed record UpdateFlightRequest(int Id, string? FlightCode, int AirlineId, int RouteId, int AircraftId, DateTime DepartureDate, DateTime EstimatedArrivalDate, int TotalCapacity, int AvailableSeats, int FlightStatusId, DateTime? RescheduledAt, DateTime CreatedAt, DateTime UpdatedAt);
