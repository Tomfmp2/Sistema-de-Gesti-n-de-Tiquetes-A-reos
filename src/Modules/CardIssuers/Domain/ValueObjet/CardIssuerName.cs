namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.ValueObjet;

public class CardIssuerName
{
    public string Value { get; }

    public CardIssuerName(string value)
    {
        Value = value;
    }

    public static CardIssuerName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Trim().Length > 50)
        {
            throw new ArgumentException("El valor no puede tener mas de 50 caracteres");
        }

        return new CardIssuerName(value.Trim());
    }
}
