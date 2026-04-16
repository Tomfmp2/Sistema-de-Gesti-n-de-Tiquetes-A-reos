namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.ValueObjet;

public sealed class UserSystemRoleId
{
    public int Value { get; }

    public UserSystemRoleId(int value) => Value = value;

    public static UserSystemRoleId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new UserSystemRoleId(value);
    }
}
