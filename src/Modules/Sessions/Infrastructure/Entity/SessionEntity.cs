namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Infrastructure.Entity;

public class SessionEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? ClosedAt { get; set; }
    public string? OriginIp { get; set; }
    public bool IsActive { get; set; }
}
