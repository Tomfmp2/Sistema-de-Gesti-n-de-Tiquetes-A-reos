namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

public class ReservationTotalValue
{
    public decimal Value { get; }

    public ReservationTotalValue(decimal value)
    {
        Value = value;
    }

    public static ReservationTotalValue Create(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("El valor total no puede ser menor a 0");
        }

        return new ReservationTotalValue(value);
    }
}
