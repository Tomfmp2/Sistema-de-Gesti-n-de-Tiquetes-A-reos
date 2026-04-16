namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;

public class PaymentReservationId
{
    public int Value { get; }

    public PaymentReservationId(int value)
    {
        Value = value;
    }

    public static PaymentReservationId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new PaymentReservationId(value);
    }
}
