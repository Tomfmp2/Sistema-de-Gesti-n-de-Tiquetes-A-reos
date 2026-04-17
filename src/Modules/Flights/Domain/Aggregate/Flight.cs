using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Aggregate;

public class Flight
{
    public FlightId Id { get; private set; }
    public FlightCode FlightCode { get; private set; }
    public FlightAirlineId AirlineId { get; private set; }
    public FlightRouteId RouteId { get; private set; }
    public FlightAircraftId AircraftId { get; private set; }
    public FlightDepartureDate DepartureDate { get; private set; }
    public FlightEstimatedArrivalDate EstimatedArrivalDate { get; private set; }
    public FlightTotalCapacity TotalCapacity { get; private set; }
    public FlightAvailableSeats AvailableSeats { get; private set; }
    public FlightStatusId FlightStatusId { get; private set; }
    public FlightRescheduledAt RescheduledAt { get; private set; }
    public FlightCreatedAt CreatedAt { get; private set; }
    public FlightUpdatedAt UpdatedAt { get; private set; }

    private Flight(
        FlightId id,
        FlightCode flightCode,
        FlightAirlineId airlineId,
        FlightRouteId routeId,
        FlightAircraftId aircraftId,
        FlightDepartureDate departureDate,
        FlightEstimatedArrivalDate estimatedArrivalDate,
        FlightTotalCapacity totalCapacity,
        FlightAvailableSeats availableSeats,
        FlightStatusId flightStatusId,
        FlightRescheduledAt rescheduledAt,
        FlightCreatedAt createdAt,
        FlightUpdatedAt updatedAt
    )
    {
        Id = id;
        FlightCode = flightCode;
        AirlineId = airlineId;
        RouteId = routeId;
        AircraftId = aircraftId;
        DepartureDate = departureDate;
        EstimatedArrivalDate = estimatedArrivalDate;
        TotalCapacity = totalCapacity;
        AvailableSeats = availableSeats;
        FlightStatusId = flightStatusId;
        RescheduledAt = rescheduledAt;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Flight Create(
        FlightId id,
        FlightCode flightCode,
        FlightAirlineId airlineId,
        FlightRouteId routeId,
        FlightAircraftId aircraftId,
        FlightDepartureDate departureDate,
        FlightEstimatedArrivalDate estimatedArrivalDate,
        FlightTotalCapacity totalCapacity,
        FlightAvailableSeats availableSeats,
        FlightStatusId flightStatusId,
        FlightRescheduledAt rescheduledAt,
        FlightCreatedAt createdAt,
        FlightUpdatedAt updatedAt
    )
    {
        return new Flight(
            id,
            flightCode,
            airlineId,
            routeId,
            aircraftId,
            departureDate,
            estimatedArrivalDate,
            totalCapacity,
            availableSeats,
            flightStatusId,
            rescheduledAt,
            createdAt,
            updatedAt
        );
    }
}
