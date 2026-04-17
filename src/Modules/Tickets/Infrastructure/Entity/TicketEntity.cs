namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.Entity;

public class TicketEntity
{
    public int Id { get; set; }
    public int ReservationPassengerId { get; set; }
    public string? Code { get; set; }
    public DateTime IssueDate { get; set; }
    public int TicketStatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
