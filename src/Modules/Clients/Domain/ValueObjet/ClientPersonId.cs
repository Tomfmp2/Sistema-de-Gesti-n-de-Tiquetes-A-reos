namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.ValueObjet;

public sealed class ClientPersonId
{
    public int Value { get; }

    public ClientPersonId(int value) => Value = value;

    public static ClientPersonId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new ClientPersonId(value);
    }
}
