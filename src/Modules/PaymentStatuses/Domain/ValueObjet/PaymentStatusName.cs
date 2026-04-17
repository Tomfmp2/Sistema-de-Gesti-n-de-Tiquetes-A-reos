namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.ValueObjet;

public class PaymentStatusName
{
    public string Value { get; }

    public PaymentStatusName(string value)
    {
        Value = value;
    }

    public static PaymentStatusName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Trim().Length > 50)
        {
            throw new ArgumentException("El valor no puede tener mas de 50 caracteres");
        }

        return new PaymentStatusName(value.Trim());
    }
}
