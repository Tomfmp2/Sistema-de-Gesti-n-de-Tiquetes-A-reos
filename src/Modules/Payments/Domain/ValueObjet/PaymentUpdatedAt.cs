namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;

public class PaymentUpdatedAt
{
    public DateTime Value { get; }

    public PaymentUpdatedAt(DateTime value)
    {
        Value = value;
    }

    public static PaymentUpdatedAt Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de actualizacion no puede ser vacia");
        }

        return new PaymentUpdatedAt(value);
    }
}
