namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.ValueObjet;

public class FlightStatusTransitionOriginStatusId
{
    public int Value { get; }

    public FlightStatusTransitionOriginStatusId(int value)
    {
        Value = value;
    }

    public static FlightStatusTransitionOriginStatusId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new FlightStatusTransitionOriginStatusId(value);
    }
}
