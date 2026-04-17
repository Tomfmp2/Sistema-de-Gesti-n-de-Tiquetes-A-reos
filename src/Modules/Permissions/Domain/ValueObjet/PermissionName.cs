namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.ValueObjet;

public sealed class PermissionName
{
    public string Value { get; }

    public PermissionName(string value) => Value = value;

    public static PermissionName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El nombre no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 100)
        {
            throw new ArgumentException("Máximo 100 caracteres.");
        }

        return new PermissionName(t);
    }
}
