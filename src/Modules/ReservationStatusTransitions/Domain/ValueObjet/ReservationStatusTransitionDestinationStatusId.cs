namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.ValueObjet;

public class ReservationStatusTransitionDestinationStatusId
{
    public int Value { get; }

    public ReservationStatusTransitionDestinationStatusId(int value)
    {
        Value = value;
    }

    public static ReservationStatusTransitionDestinationStatusId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new ReservationStatusTransitionDestinationStatusId(value);
    }
}
