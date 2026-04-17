namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;

public class PaymentPaymentStatusId
{
    public int Value { get; }

    public PaymentPaymentStatusId(int value)
    {
        Value = value;
    }

    public static PaymentPaymentStatusId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new PaymentPaymentStatusId(value);
    }
}
