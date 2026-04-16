namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.ValueObjet;

public class PaymentMethodCommercialName
{
    public string Value { get; }

    public PaymentMethodCommercialName(string value)
    {
        Value = value;
    }

    public static PaymentMethodCommercialName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Trim().Length > 50)
        {
            throw new ArgumentException("El valor no puede tener mas de 50 caracteres");
        }

        return new PaymentMethodCommercialName(value.Trim());
    }
}
