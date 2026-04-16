namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.ValueObjet;

public class CardIssuerId
{
    public int Value { get; }

    public CardIssuerId(int value)
    {
        Value = value;
    }

    public static CardIssuerId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new CardIssuerId(value);
    }
}
