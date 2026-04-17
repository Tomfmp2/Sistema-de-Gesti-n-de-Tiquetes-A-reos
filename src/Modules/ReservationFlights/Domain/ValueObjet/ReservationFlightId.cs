namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.ValueObjet;

public class ReservationFlightId
{
    public int Value { get; }

    public ReservationFlightId(int value)
    {
        Value = value;
    }

    public static ReservationFlightId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new ReservationFlightId(value);
    }
}
