namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;

public class UserEntity
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    public int? PersonId { get; set; }
    public int SystemRoleId { get; set; }
    public bool IsActive { get; set; }
    public DateTime? LastAccessAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
