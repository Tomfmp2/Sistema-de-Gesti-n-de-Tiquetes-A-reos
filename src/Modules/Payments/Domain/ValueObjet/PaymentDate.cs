namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;

public class PaymentDate
{
    public DateTime Value { get; }

    public PaymentDate(DateTime value)
    {
        Value = value;
    }

    public static PaymentDate Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de pago no puede ser vacia");
        }

        return new PaymentDate(value);
    }
}
