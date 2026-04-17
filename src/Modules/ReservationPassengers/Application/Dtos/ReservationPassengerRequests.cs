namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.Dtos;

public sealed record CreateReservationPassengerRequest(int ReservationFlightId, int PassengerId);

public sealed record UpdateReservationPassengerRequest(int Id, int ReservationFlightId, int PassengerId);
