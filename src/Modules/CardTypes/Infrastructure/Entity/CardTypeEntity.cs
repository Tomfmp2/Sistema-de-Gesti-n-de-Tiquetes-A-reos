using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Entity;

public class CardTypeEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<PaymentMethodEntity> PaymentMethods { get; set; } =
        new List<PaymentMethodEntity>();
}
