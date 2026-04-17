using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Aggregate;

public sealed class Client
{
    public ClientId Id { get; private set; }
    public ClientPersonId PersonId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Client(ClientId id, ClientPersonId personId, DateTime createdAt)
    {
        Id = id;
        PersonId = personId;
        CreatedAt = createdAt;
    }

    public static Client CreateNew(ClientPersonId personId)
    {
        return new Client(ClientId.Unpersisted, personId, default);
    }

    public static Client Create(ClientId id, ClientPersonId personId, DateTime createdAt)
    {
        return new Client(id, personId, createdAt);
    }
}
