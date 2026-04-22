using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Entity;

public class PermissionEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public ICollection<RolePermissionEntity> RolePermissions { get; set; } =
        new List<RolePermissionEntity>();
}
