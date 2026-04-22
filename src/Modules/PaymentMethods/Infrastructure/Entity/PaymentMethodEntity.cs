using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Entity;

public class PaymentMethodEntity
{
    public int Id { get; set; }
    public int PaymentMethodTypeId { get; set; }
    public int? CardTypeId { get; set; }
    public int? CardIssuerId { get; set; }
    public string? CommercialName { get; set; }

    public PaymentMethodTypeEntity? PaymentMethodType { get; set; }
    public CardTypeEntity? CardType { get; set; }
    public CardIssuerEntity? CardIssuer { get; set; }
    public ICollection<PaymentEntity> Payments { get; set; } = new List<PaymentEntity>();
}
