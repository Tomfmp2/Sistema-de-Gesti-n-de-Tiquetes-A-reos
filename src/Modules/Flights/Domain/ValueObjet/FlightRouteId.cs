namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightRouteId
{
    public int Value { get; }

    public FlightRouteId(int value)
    {
        Value = value;
    }

    public static FlightRouteId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new FlightRouteId(value);
    }
}
