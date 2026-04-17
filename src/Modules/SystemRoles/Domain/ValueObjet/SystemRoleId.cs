namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.ValueObjet;

public sealed class SystemRoleId
{
    public int Value { get; }

    public SystemRoleId(int value) => Value = value;

    public static SystemRoleId Unpersisted => new(0);

    public static SystemRoleId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new SystemRoleId(value);
    }
}
