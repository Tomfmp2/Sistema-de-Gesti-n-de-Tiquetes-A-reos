using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Aggregate;

public class CardIssuer
{
    public CardIssuerId Id { get; private set; }
    public CardIssuerName Name { get; private set; }

    private CardIssuer(
        CardIssuerId id,
        CardIssuerName name
    )
    {
        Id = id;
        Name = name;
    }

    public static CardIssuer Create(
        CardIssuerId id,
        CardIssuerName name
    )
    {
        return new CardIssuer(
            id,
            name
        );
    }
}
