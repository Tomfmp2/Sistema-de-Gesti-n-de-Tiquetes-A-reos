namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Auth.Infrastructure.Entity;

public class UserEntity
{
    public int Id { get; set; }
    public int? PersonId { get; set; }
    public int SystemRoleId { get; set; }
    public string? Username { get; set; }
    public string? PasswordHash { get; set; }
    public bool IsActive { get; set; }
}
