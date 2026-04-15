namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightEstimatedArrivalDate
{
    public DateTime Value { get; }

    public FlightEstimatedArrivalDate(DateTime value)
    {
        Value = value;
    }

    public static FlightEstimatedArrivalDate Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha estimada de llegada no puede ser vacia");
        }

        return new FlightEstimatedArrivalDate(value);
    }
}
