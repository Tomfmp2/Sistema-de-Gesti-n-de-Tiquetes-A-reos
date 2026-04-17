namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.ValueObjet;

public class ReservationFlightFlightId
{
    public int Value { get; }

    public ReservationFlightFlightId(int value)
    {
        Value = value;
    }

    public static ReservationFlightFlightId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new ReservationFlightFlightId(value);
    }
}
