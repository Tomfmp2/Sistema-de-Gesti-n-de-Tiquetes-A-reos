namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightDepartureDate
{
    public DateTime Value { get; }

    public FlightDepartureDate(DateTime value)
    {
        Value = value;
    }

    public static FlightDepartureDate Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de salida no puede ser vacia");
        }

        return new FlightDepartureDate(value);
    }
}
