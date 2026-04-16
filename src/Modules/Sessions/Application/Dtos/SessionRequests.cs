namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.Dtos;

public sealed record CreateSessionRequest(
    int UserId,
    DateTime StartedAt,
    DateTime? ClosedAt,
    string? OriginIp,
    bool IsActive
);

public sealed record UpdateSessionRequest(
    int Id,
    int UserId,
    DateTime StartedAt,
    DateTime? ClosedAt,
    string? OriginIp,
    bool IsActive
);
