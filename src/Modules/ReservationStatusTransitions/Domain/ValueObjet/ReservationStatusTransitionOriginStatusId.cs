namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.ValueObjet;

public class ReservationStatusTransitionOriginStatusId
{
    public int Value { get; }

    public ReservationStatusTransitionOriginStatusId(int value)
    {
        Value = value;
    }

    public static ReservationStatusTransitionOriginStatusId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new ReservationStatusTransitionOriginStatusId(value);
    }
}
