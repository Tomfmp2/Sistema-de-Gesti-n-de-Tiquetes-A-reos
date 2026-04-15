namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

public class ReservationDate
{
    public DateTime Value { get; }

    public ReservationDate(DateTime value)
    {
        Value = value;
    }

    public static ReservationDate Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de reserva no puede ser vacia");
        }

        return new ReservationDate(value);
    }
}
