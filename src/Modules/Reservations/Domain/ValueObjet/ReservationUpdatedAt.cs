namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

public class ReservationUpdatedAt
{
    public DateTime Value { get; }

    public ReservationUpdatedAt(DateTime value)
    {
        Value = value;
    }

    public static ReservationUpdatedAt Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de actualizacion no puede ser vacia");
        }

        return new ReservationUpdatedAt(value);
    }
}
