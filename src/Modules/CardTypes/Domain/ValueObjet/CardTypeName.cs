namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.ValueObjet;

public class CardTypeName
{
    public string Value { get; }

    public CardTypeName(string value)
    {
        Value = value;
    }

    public static CardTypeName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Trim().Length > 50)
        {
            throw new ArgumentException("El valor no puede tener mas de 50 caracteres");
        }

        return new CardTypeName(value.Trim());
    }
}
