namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.ValueObjet;

public class PaymentMethodId
{
    public int Value { get; }

    public PaymentMethodId(int value)
    {
        Value = value;
    }

    public static PaymentMethodId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new PaymentMethodId(value);
    }
}
