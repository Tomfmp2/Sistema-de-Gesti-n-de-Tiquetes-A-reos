using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Aggregate;

public sealed class SystemRole
{
    public SystemRoleId Id { get; private set; }
    public SystemRoleName Name { get; private set; }
    public string? Description { get; private set; }

    private SystemRole(SystemRoleId id, SystemRoleName name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public static SystemRole CreateNew(SystemRoleName name, string? description)
    {
        return new SystemRole(SystemRoleId.Unpersisted, name, NormalizeDescription(description));
    }

    public static SystemRole Create(SystemRoleId id, SystemRoleName name, string? description)
    {
        return new SystemRole(id, name, NormalizeDescription(description));
    }

    private static string? NormalizeDescription(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var t = value.Trim();
        if (t.Length > 150)
        {
            throw new ArgumentException("La descripción no puede superar 150 caracteres.");
        }

        return t;
    }
}
