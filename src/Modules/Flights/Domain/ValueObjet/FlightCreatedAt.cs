namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightCreatedAt
{
    public DateTime Value { get; }

    public FlightCreatedAt(DateTime value)
    {
        Value = value;
    }

    public static FlightCreatedAt Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de creacion no puede ser vacia");
        }

        return new FlightCreatedAt(value);
    }
}
