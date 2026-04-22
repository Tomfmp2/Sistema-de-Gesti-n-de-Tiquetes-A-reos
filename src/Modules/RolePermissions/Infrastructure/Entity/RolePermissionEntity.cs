namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Infrastructure.Entity;

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Entity;

public class RolePermissionEntity
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    public SystemRoleEntity? Role { get; set; }
    public PermissionEntity? Permission { get; set; }
}
