namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

public sealed record AuthContext(
    int UserId,
    string Username,
    int SessionId,
    int RoleId,
    string RoleName,
    int? ClientId
);

public enum AppExitReason
{
    Logout = 1
}

