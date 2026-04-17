namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.Dtos;

public sealed record CreatePermissionRequest(string Name, string? Description);

public sealed record UpdatePermissionRequest(int Id, string Name, string? Description);
