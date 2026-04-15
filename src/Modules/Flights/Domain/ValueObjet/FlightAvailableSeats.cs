namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightAvailableSeats
{
    public int Value { get; }

    public FlightAvailableSeats(int value)
    {
        Value = value;
    }

    public static FlightAvailableSeats Create(int value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Los asientos disponibles no pueden ser menores a 0");
        }

        return new FlightAvailableSeats(value);
    }
}
