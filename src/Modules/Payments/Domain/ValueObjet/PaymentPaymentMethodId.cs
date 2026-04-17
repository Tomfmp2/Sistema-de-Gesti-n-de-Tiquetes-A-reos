namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;

public class PaymentPaymentMethodId
{
    public int Value { get; }

    public PaymentPaymentMethodId(int value)
    {
        Value = value;
    }

    public static PaymentPaymentMethodId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new PaymentPaymentMethodId(value);
    }
}
