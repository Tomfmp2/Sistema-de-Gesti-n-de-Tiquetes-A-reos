namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.ValueObjet;

public class FlightStatusTransitionDestinationStatusId
{
    public int Value { get; }

    public FlightStatusTransitionDestinationStatusId(int value)
    {
        Value = value;
    }

    public static FlightStatusTransitionDestinationStatusId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new FlightStatusTransitionDestinationStatusId(value);
    }
}
