namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.ValueObjet;

public class PaymentMethodPaymentMethodTypeId
{
    public int Value { get; }

    public PaymentMethodPaymentMethodTypeId(int value)
    {
        Value = value;
    }

    public static PaymentMethodPaymentMethodTypeId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new PaymentMethodPaymentMethodTypeId(value);
    }
}
