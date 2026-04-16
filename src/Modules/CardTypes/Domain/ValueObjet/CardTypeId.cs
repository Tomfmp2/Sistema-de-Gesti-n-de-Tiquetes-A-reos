namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.ValueObjet;

public class CardTypeId
{
    public int Value { get; }

    public CardTypeId(int value)
    {
        Value = value;
    }

    public static CardTypeId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new CardTypeId(value);
    }
}
