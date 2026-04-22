using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Entity;

public class PaymentStatusEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<PaymentEntity> Payments { get; set; } = new List<PaymentEntity>();
}
