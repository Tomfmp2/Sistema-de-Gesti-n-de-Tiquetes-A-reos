using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Data;

public static class FlightDefaultData
{
    private static readonly DateTime SeedTimestamp = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    // TotalCapacity / AvailableSeats deben coincidir con asientos generados (FlightSeatLayoutGenerator).
    public static readonly FlightEntity[] Flights =
    [
        new() { Id = 1, FlightCode = "AV101", AirlineId = 1, RouteId = 1, AircraftId = 1, DepartureDate = new DateTime(2026, 6, 1, 8, 0, 0), EstimatedArrivalDate = new DateTime(2026, 6, 1, 8, 55, 0), TotalCapacity = 180, AvailableSeats = 180, FlightStatusId = 1, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 2, FlightCode = "AV201", AirlineId = 1, RouteId = 4, AircraftId = 2, DepartureDate = new DateTime(2026, 6, 1, 9, 0, 0), EstimatedArrivalDate = new DateTime(2026, 6, 1, 12, 45, 0), TotalCapacity = 180, AvailableSeats = 180, FlightStatusId = 1, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 3, FlightCode = "LA301", AirlineId = 2, RouteId = 14, AircraftId = 3, DepartureDate = new DateTime(2026, 6, 2, 7, 30, 0), EstimatedArrivalDate = new DateTime(2026, 6, 2, 11, 10, 0), TotalCapacity = 186, AvailableSeats = 186, FlightStatusId = 1, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 4, FlightCode = "AA401", AirlineId = 3, RouteId = 8, AircraftId = 4, DepartureDate = new DateTime(2026, 6, 2, 13, 0, 0), EstimatedArrivalDate = new DateTime(2026, 6, 2, 16, 0, 0), TotalCapacity = 192, AvailableSeats = 192, FlightStatusId = 1, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        new() { Id = 5, FlightCode = "IB501", AirlineId = 4, RouteId = 11, AircraftId = 5, DepartureDate = new DateTime(2026, 6, 3, 10, 0, 0), EstimatedArrivalDate = new DateTime(2026, 6, 3, 12, 25, 0), TotalCapacity = 316, AvailableSeats = 316, FlightStatusId = 1, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp },
        // Vuelo demo: avión 7 (100 económica + 20 ejecutiva + 10 primera)
        new() { Id = 6, FlightCode = "DEMO777", AirlineId = 1, RouteId = 1, AircraftId = 7, DepartureDate = new DateTime(2026, 7, 15, 14, 0, 0), EstimatedArrivalDate = new DateTime(2026, 7, 15, 16, 30, 0), TotalCapacity = 130, AvailableSeats = 130, FlightStatusId = 1, CreatedAt = SeedTimestamp, UpdatedAt = SeedTimestamp }
    ];
}
