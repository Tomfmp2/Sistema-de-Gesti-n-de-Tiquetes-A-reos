using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Aggregate;

public sealed class RolePermission
{
    public RolePermissionId Id { get; private set; }
    public RolePermissionRoleId RoleId { get; private set; }
    public RolePermissionPermissionId PermissionId { get; private set; }

    private RolePermission(
        RolePermissionId id,
        RolePermissionRoleId roleId,
        RolePermissionPermissionId permissionId
    )
    {
        Id = id;
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public static RolePermission CreateNew(
        RolePermissionRoleId roleId,
        RolePermissionPermissionId permissionId
    )
    {
        return new RolePermission(RolePermissionId.Unpersisted, roleId, permissionId);
    }

    public static RolePermission Create(
        RolePermissionId id,
        RolePermissionRoleId roleId,
        RolePermissionPermissionId permissionId
    )
    {
        return new RolePermission(id, roleId, permissionId);
    }
}
