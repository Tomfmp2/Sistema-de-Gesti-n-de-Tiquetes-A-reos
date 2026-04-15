namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightStatusId
{
    public int Value { get; }

    public FlightStatusId(int value)
    {
        Value = value;
    }

    public static FlightStatusId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new FlightStatusId(value);
    }
}
