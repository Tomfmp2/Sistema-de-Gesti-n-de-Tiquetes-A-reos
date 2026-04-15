namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.ValueObjet;

public class ReservationFlightReservationId
{
    public int Value { get; }

    public ReservationFlightReservationId(int value)
    {
        Value = value;
    }

    public static ReservationFlightReservationId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new ReservationFlightReservationId(value);
    }
}
