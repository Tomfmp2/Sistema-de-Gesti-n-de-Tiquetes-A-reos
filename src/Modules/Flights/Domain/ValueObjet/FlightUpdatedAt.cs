namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightUpdatedAt
{
    public DateTime Value { get; }

    public FlightUpdatedAt(DateTime value)
    {
        Value = value;
    }

    public static FlightUpdatedAt Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de actualizacion no puede ser vacia");
        }

        return new FlightUpdatedAt(value);
    }
}
