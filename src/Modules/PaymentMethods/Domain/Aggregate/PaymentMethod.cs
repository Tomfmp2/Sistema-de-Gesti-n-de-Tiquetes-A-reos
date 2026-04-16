using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Aggregate;

public class PaymentMethod
{
    public PaymentMethodId Id { get; private set; }
    public PaymentMethodPaymentMethodTypeId PaymentMethodTypeId { get; private set; }
    public PaymentMethodCardTypeId CardTypeId { get; private set; }
    public PaymentMethodCardIssuerId CardIssuerId { get; private set; }
    public PaymentMethodCommercialName CommercialName { get; private set; }

    private PaymentMethod(
        PaymentMethodId id,
        PaymentMethodPaymentMethodTypeId paymentMethodTypeId,
        PaymentMethodCardTypeId cardTypeId,
        PaymentMethodCardIssuerId cardIssuerId,
        PaymentMethodCommercialName commercialName
    )
    {
        Id = id;
        PaymentMethodTypeId = paymentMethodTypeId;
        CardTypeId = cardTypeId;
        CardIssuerId = cardIssuerId;
        CommercialName = commercialName;
    }

    public static PaymentMethod Create(
        PaymentMethodId id,
        PaymentMethodPaymentMethodTypeId paymentMethodTypeId,
        PaymentMethodCardTypeId cardTypeId,
        PaymentMethodCardIssuerId cardIssuerId,
        PaymentMethodCommercialName commercialName
    )
    {
        return new PaymentMethod(
            id,
            paymentMethodTypeId,
            cardTypeId,
            cardIssuerId,
            commercialName
        );
    }
}
