namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

public class ReservationCreatedAt
{
    public DateTime Value { get; }

    public ReservationCreatedAt(DateTime value)
    {
        Value = value;
    }

    public static ReservationCreatedAt Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de creacion no puede ser vacia");
        }

        return new ReservationCreatedAt(value);
    }
}
