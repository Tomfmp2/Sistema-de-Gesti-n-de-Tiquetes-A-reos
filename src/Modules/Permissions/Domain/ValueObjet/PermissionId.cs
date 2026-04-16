namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.ValueObjet;

public sealed class PermissionId
{
    public int Value { get; }

    public PermissionId(int value) => Value = value;

    public static PermissionId Unpersisted => new(0);

    public static PermissionId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PermissionId(value);
    }
}
