using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Aggregate;

public class ReservationPassenger
{
    public ReservationPassengerId Id { get; private set; }
    public ReservationPassengerReservationFlightId ReservationFlightId { get; private set; }
    public ReservationPassengerPassengerId PassengerId { get; private set; }
    public ReservationPassengerCabinTypeId CabinTypeId { get; private set; }

    private ReservationPassenger(
        ReservationPassengerId id,
        ReservationPassengerReservationFlightId reservationFlightId,
        ReservationPassengerPassengerId passengerId,
        ReservationPassengerCabinTypeId cabinTypeId
    )
    {
        Id = id;
        ReservationFlightId = reservationFlightId;
        PassengerId = passengerId;
        CabinTypeId = cabinTypeId;
    }

    public static ReservationPassenger Create(
        ReservationPassengerId id,
        ReservationPassengerReservationFlightId reservationFlightId,
        ReservationPassengerPassengerId passengerId,
        ReservationPassengerCabinTypeId cabinTypeId
    )
    {
        return new ReservationPassenger(id, reservationFlightId, passengerId, cabinTypeId);
    }
}
