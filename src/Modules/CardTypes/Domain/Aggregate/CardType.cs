using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Aggregate;

public class CardType
{
    public CardTypeId Id { get; private set; }
    public CardTypeName Name { get; private set; }

    private CardType(
        CardTypeId id,
        CardTypeName name
    )
    {
        Id = id;
        Name = name;
    }

    public static CardType Create(
        CardTypeId id,
        CardTypeName name
    )
    {
        return new CardType(
            id,
            name
        );
    }
}
