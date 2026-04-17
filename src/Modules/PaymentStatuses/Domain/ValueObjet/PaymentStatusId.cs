namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.ValueObjet;

public class PaymentStatusId
{
    public int Value { get; }

    public PaymentStatusId(int value)
    {
        Value = value;
    }

    public static PaymentStatusId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new PaymentStatusId(value);
    }
}
