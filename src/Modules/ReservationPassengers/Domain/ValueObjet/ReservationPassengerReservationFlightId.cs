namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.ValueObjet;

public class ReservationPassengerReservationFlightId
{
    public int Value { get; }

    public ReservationPassengerReservationFlightId(int value)
    {
        Value = value;
    }

    public static ReservationPassengerReservationFlightId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new ReservationPassengerReservationFlightId(value);
    }
}
