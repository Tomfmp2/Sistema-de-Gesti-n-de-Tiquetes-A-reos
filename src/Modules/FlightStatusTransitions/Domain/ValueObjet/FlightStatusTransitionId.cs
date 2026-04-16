namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.ValueObjet;

public class FlightStatusTransitionId
{
    public int Value { get; }

    public FlightStatusTransitionId(int value)
    {
        Value = value;
    }

    public static FlightStatusTransitionId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new FlightStatusTransitionId(value);
    }
}
