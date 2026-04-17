namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.ValueObjet;

public sealed class RolePermissionRoleId
{
    public int Value { get; }

    public RolePermissionRoleId(int value) => Value = value;

    public static RolePermissionRoleId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new RolePermissionRoleId(value);
    }
}
