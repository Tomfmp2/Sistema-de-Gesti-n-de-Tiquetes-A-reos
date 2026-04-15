namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightId
{
    public int Value { get; }

    public FlightId(int value)
    {
        Value = value;
    }

    public static FlightId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new FlightId(value);
    }
}
