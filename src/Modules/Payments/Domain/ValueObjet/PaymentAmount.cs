namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;

public class PaymentAmount
{
    public decimal Value { get; }

    public PaymentAmount(decimal value)
    {
        Value = value;
    }

    public static PaymentAmount Create(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("El monto no puede ser menor a 0");
        }

        return new PaymentAmount(value);
    }
}
