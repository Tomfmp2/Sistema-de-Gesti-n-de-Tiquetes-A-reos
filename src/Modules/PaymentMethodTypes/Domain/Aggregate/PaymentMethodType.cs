using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Aggregate;

public class PaymentMethodType
{
    public PaymentMethodTypeId Id { get; private set; }
    public PaymentMethodTypeName Name { get; private set; }

    private PaymentMethodType(
        PaymentMethodTypeId id,
        PaymentMethodTypeName name
    )
    {
        Id = id;
        Name = name;
    }

    public static PaymentMethodType Create(
        PaymentMethodTypeId id,
        PaymentMethodTypeName name
    )
    {
        return new PaymentMethodType(
            id,
            name
        );
    }
}
