namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.ValueObjet;

public sealed class RolePermissionId
{
    public int Value { get; }

    public RolePermissionId(int value) => Value = value;

    public static RolePermissionId Unpersisted => new(0);

    public static RolePermissionId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new RolePermissionId(value);
    }
}
