namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.ValueObjet;

public sealed class SessionUserId
{
    public int Value { get; }

    public SessionUserId(int value) => Value = value;

    public static SessionUserId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new SessionUserId(value);
    }
}
