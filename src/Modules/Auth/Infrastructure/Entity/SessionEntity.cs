namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Auth.Infrastructure.Entity;

public class SessionEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime IssuedAtUtc { get; set; }
    public DateTime ExpiresAtUtc { get; set; }
    public bool IsRevoked { get; set; }
    public string? RefreshToken { get; set; }
}
