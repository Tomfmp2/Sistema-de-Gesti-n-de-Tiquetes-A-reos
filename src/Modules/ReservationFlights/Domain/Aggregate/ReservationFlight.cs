using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Aggregate;

public class ReservationFlight
{
    public ReservationFlightId Id { get; private set; }
    public ReservationFlightReservationId ReservationId { get; private set; }
    public ReservationFlightFlightId FlightId { get; private set; }
    public ReservationFlightPartialValue PartialValue { get; private set; }

    private ReservationFlight(
        ReservationFlightId id,
        ReservationFlightReservationId reservationId,
        ReservationFlightFlightId flightId,
        ReservationFlightPartialValue partialValue
    )
    {
        Id = id;
        ReservationId = reservationId;
        FlightId = flightId;
        PartialValue = partialValue;
    }

    public static ReservationFlight Create(
        ReservationFlightId id,
        ReservationFlightReservationId reservationId,
        ReservationFlightFlightId flightId,
        ReservationFlightPartialValue partialValue
    )
    {
        return new ReservationFlight(id, reservationId, flightId, partialValue);
    }
}
