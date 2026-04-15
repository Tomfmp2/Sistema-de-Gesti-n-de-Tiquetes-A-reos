namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightAirlineId
{
    public int Value { get; }

    public FlightAirlineId(int value)
    {
        Value = value;
    }

    public static FlightAirlineId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new FlightAirlineId(value);
    }
}
