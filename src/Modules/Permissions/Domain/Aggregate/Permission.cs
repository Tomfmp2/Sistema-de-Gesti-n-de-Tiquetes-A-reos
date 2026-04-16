using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Aggregate;

public sealed class Permission
{
    public PermissionId Id { get; private set; }
    public PermissionName Name { get; private set; }
    public string? Description { get; private set; }

    private Permission(PermissionId id, PermissionName name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public static Permission CreateNew(PermissionName name, string? description)
    {
        return new Permission(PermissionId.Unpersisted, name, NormalizeDescription(description));
    }

    public static Permission Create(PermissionId id, PermissionName name, string? description)
    {
        return new Permission(id, name, NormalizeDescription(description));
    }

    private static string? NormalizeDescription(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var t = value.Trim();
        if (t.Length > 200)
        {
            throw new ArgumentException("La descripción no puede superar 200 caracteres.");
        }

        return t;
    }
}
