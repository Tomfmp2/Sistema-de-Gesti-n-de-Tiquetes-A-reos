using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Aggregate;

public sealed class Session
{
    public SessionId Id { get; private set; }
    public SessionUserId UserId { get; private set; }
    public DateTime StartedAt { get; private set; }
    public DateTime? ClosedAt { get; private set; }
    public string? OriginIp { get; private set; }
    public bool IsActive { get; private set; }

    private Session(
        SessionId id,
        SessionUserId userId,
        DateTime startedAt,
        DateTime? closedAt,
        string? originIp,
        bool isActive
    )
    {
        Id = id;
        UserId = userId;
        StartedAt = startedAt;
        ClosedAt = closedAt;
        OriginIp = originIp;
        IsActive = isActive;
    }

    public static Session CreateNew(
        SessionUserId userId,
        DateTime startedAt,
        DateTime? closedAt,
        string? originIp,
        bool isActive
    )
    {
        return new Session(
            SessionId.Unpersisted,
            userId,
            startedAt,
            closedAt,
            NormalizeOriginIp(originIp),
            isActive
        );
    }

    public static Session Create(
        SessionId id,
        SessionUserId userId,
        DateTime startedAt,
        DateTime? closedAt,
        string? originIp,
        bool isActive
    )
    {
        return new Session(
            id,
            userId,
            startedAt,
            closedAt,
            NormalizeOriginIp(originIp),
            isActive
        );
    }

    private static string? NormalizeOriginIp(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var t = value.Trim();
        if (t.Length > 45)
        {
            throw new ArgumentException("origin_ip no puede superar 45 caracteres.");
        }

        return t;
    }
}
