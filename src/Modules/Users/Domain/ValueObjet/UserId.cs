namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.ValueObjet;

public sealed class UserId
{
    public int Value { get; }

    public UserId(int value) => Value = value;

    public static UserId Unpersisted => new(0);

    public static UserId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new UserId(value);
    }
}
