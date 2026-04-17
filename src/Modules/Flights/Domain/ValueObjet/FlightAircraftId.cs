namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightAircraftId
{
    public int Value { get; }

    public FlightAircraftId(int value)
    {
        Value = value;
    }

    public static FlightAircraftId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new FlightAircraftId(value);
    }
}
