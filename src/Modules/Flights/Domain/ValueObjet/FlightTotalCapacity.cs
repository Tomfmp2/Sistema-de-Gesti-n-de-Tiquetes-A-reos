namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightTotalCapacity
{
    public int Value { get; }

    public FlightTotalCapacity(int value)
    {
        Value = value;
    }

    public static FlightTotalCapacity Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("La capacidad total debe ser mayor a 0");
        }

        return new FlightTotalCapacity(value);
    }
}
