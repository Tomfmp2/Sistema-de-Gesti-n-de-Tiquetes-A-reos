namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.ValueObjet;

public sealed class RolePermissionPermissionId
{
    public int Value { get; }

    public RolePermissionPermissionId(int value) => Value = value;

    public static RolePermissionPermissionId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new RolePermissionPermissionId(value);
    }
}
