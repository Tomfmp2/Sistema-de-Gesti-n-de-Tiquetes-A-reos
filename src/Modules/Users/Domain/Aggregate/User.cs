using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.Aggregate;

public sealed class User
{
    public UserId Id { get; private set; }
    public UserUsername Username { get; private set; }
    public UserPasswordHash PasswordHash { get; private set; }
    public int? PersonId { get; private set; }
    public UserSystemRoleId SystemRoleId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime? LastAccessAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private User(
        UserId id,
        UserUsername username,
        UserPasswordHash passwordHash,
        int? personId,
        UserSystemRoleId systemRoleId,
        bool isActive,
        DateTime? lastAccessAt,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        Id = id;
        Username = username;
        PasswordHash = passwordHash;
        PersonId = personId;
        SystemRoleId = systemRoleId;
        IsActive = isActive;
        LastAccessAt = lastAccessAt;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static User CreateNew(
        UserUsername username,
        UserPasswordHash passwordHash,
        int? personId,
        UserSystemRoleId systemRoleId,
        bool isActive
    )
    {
        return new User(
            UserId.Unpersisted,
            username,
            passwordHash,
            personId,
            systemRoleId,
            isActive,
            null,
            default,
            default
        );
    }

    public static User Create(
        UserId id,
        UserUsername username,
        UserPasswordHash passwordHash,
        int? personId,
        UserSystemRoleId systemRoleId,
        bool isActive,
        DateTime? lastAccessAt,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new User(
            id,
            username,
            passwordHash,
            personId,
            systemRoleId,
            isActive,
            lastAccessAt,
            createdAt,
            updatedAt
        );
    }
}
