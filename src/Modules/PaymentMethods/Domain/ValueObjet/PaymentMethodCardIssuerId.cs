namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.ValueObjet;

public class PaymentMethodCardIssuerId
{
    public int? Value { get; }

    public PaymentMethodCardIssuerId(int? value)
    {
        Value = value;
    }

    public static PaymentMethodCardIssuerId Create(int? value)
    {
        if (value.HasValue && value.Value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new PaymentMethodCardIssuerId(value);
    }
}
