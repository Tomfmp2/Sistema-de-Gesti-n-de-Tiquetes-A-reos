namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Application.Dtos;

public sealed record CreateUserRequest(
    string Username,
    string PasswordHash,
    int? PersonId,
    int SystemRoleId,
    bool IsActive
);

public sealed record UpdateUserRequest(
    int Id,
    string Username,
    string PasswordHash,
    int? PersonId,
    int SystemRoleId,
    bool IsActive,
    DateTime? LastAccessAt
);
