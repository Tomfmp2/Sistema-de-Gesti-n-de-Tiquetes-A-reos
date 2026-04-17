namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.ValueObjet;

public sealed class ClientId
{
    public int Value { get; }

    public ClientId(int value) => Value = value;

    public static ClientId Unpersisted => new(0);

    public static ClientId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new ClientId(value);
    }
}
