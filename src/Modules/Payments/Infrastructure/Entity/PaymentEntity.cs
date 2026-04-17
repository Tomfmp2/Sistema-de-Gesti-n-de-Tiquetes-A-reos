using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Infrastructure.Entity;

public class PaymentEntity
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int PaymentStatusId { get; set; }
    public int PaymentMethodId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public ReservationEntity? Reservation { get; set; }
    public PaymentStatusEntity? PaymentStatus { get; set; }
    public PaymentMethodEntity? PaymentMethod { get; set; }
}
