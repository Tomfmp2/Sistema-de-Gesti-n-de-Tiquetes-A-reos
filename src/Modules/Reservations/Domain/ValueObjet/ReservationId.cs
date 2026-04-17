namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

public class ReservationId
{
    public int Value { get; }

    public ReservationId(int value)
    {
        Value = value;
    }

    public static ReservationId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new ReservationId(value);
    }
}
