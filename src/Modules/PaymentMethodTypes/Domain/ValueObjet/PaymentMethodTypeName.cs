namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.ValueObjet;

public class PaymentMethodTypeName
{
    public string Value { get; }

    public PaymentMethodTypeName(string value)
    {
        Value = value;
    }

    public static PaymentMethodTypeName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Trim().Length > 50)
        {
            throw new ArgumentException("El valor no puede tener mas de 50 caracteres");
        }

        return new PaymentMethodTypeName(value.Trim());
    }
}
