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
}
