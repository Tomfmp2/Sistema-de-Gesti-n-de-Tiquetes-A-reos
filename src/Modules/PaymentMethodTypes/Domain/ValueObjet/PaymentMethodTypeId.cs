namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.ValueObjet;

public class PaymentMethodTypeId
{
    public int Value { get; }

    public PaymentMethodTypeId(int value)
    {
        Value = value;
    }

    public static PaymentMethodTypeId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new PaymentMethodTypeId(value);
    }
}
