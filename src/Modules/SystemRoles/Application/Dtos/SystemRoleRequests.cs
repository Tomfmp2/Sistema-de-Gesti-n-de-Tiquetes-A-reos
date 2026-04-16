namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.Dtos;

public sealed record CreateSystemRoleRequest(string Name, string? Description);

public sealed record UpdateSystemRoleRequest(int Id, string Name, string? Description);
