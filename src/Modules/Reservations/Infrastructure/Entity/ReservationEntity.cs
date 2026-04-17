namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;

public class ReservationEntity
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int ReservationStatusId { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
