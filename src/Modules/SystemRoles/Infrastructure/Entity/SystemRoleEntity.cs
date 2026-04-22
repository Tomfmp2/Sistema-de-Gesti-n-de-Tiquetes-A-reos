using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Entity;

public class SystemRoleEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    public ICollection<RolePermissionEntity> RolePermissions { get; set; } =
        new List<RolePermissionEntity>();
}
