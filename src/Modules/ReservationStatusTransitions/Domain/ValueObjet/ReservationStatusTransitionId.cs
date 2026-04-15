namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.ValueObjet;

public class ReservationStatusTransitionId
{
    public int Value { get; }

    public ReservationStatusTransitionId(int value)
    {
        Value = value;
    }

    public static ReservationStatusTransitionId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new ReservationStatusTransitionId(value);
    }
}
