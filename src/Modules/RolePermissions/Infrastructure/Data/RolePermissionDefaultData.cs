using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Infrastructure.Data;

public static class RolePermissionDefaultData
{
    public static readonly RolePermissionEntity[] RolePermissions =
    [
        new() { Id = 1, RoleId = 1, PermissionId = 1 },
        new() { Id = 2, RoleId = 1, PermissionId = 2 },
        new() { Id = 3, RoleId = 1, PermissionId = 3 },
        new() { Id = 4, RoleId = 1, PermissionId = 4 },
        new() { Id = 5, RoleId = 1, PermissionId = 5 },
        new() { Id = 6, RoleId = 2, PermissionId = 1 },
        new() { Id = 7, RoleId = 2, PermissionId = 4 },
        new() { Id = 8, RoleId = 3, PermissionId = 1 },
        new() { Id = 9, RoleId = 4, PermissionId = 2 },
        new() { Id = 10, RoleId = 4, PermissionId = 5 }
    ];
}
