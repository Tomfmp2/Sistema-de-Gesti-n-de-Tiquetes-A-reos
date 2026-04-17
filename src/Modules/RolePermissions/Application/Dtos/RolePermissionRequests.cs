namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.Dtos;

public sealed record CreateRolePermissionRequest(int RoleId, int PermissionId);

public sealed record UpdateRolePermissionRequest(int Id, int RoleId, int PermissionId);
