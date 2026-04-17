namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.Dtos;

public sealed record CreateReservationFlightRequest(int ReservationId, int FlightId, decimal PartialValue);

public sealed record UpdateReservationFlightRequest(int Id, int ReservationId, int FlightId, decimal PartialValue);
