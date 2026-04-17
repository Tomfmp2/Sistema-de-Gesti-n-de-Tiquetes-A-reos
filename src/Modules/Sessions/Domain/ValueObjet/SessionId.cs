namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.ValueObjet;

public sealed class SessionId
{
    public int Value { get; }

    public SessionId(int value) => Value = value;

    public static SessionId Unpersisted => new(0);

    public static SessionId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new SessionId(value);
    }
}
