using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Aggregate;

public class PaymentStatus
{
    public PaymentStatusId Id { get; private set; }
    public PaymentStatusName Name { get; private set; }

    private PaymentStatus(
        PaymentStatusId id,
        PaymentStatusName name
    )
    {
        Id = id;
        Name = name;
    }

    public static PaymentStatus Create(
        PaymentStatusId id,
        PaymentStatusName name
    )
    {
        return new PaymentStatus(
            id,
            name
        );
    }
}
