namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightRescheduledAt
{
    public DateTime? Value { get; }

    public FlightRescheduledAt(DateTime? value)
    {
        Value = value;
    }

    public static FlightRescheduledAt Create(DateTime? value)
    {
        return new FlightRescheduledAt(value);
    }
}
