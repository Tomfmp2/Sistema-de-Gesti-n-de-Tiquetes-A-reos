namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

public class ReservationExpiresAt
{
    public DateTime? Value { get; }

    public ReservationExpiresAt(DateTime? value)
    {
        Value = value;
    }

    public static ReservationExpiresAt Create(DateTime? value)
    {
        return new ReservationExpiresAt(value);
    }
}
