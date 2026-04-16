namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;

public class PaymentCreatedAt
{
    public DateTime Value { get; }

    public PaymentCreatedAt(DateTime value)
    {
        Value = value;
    }

    public static PaymentCreatedAt Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de creacion no puede ser vacia");
        }

        return new PaymentCreatedAt(value);
    }
}
