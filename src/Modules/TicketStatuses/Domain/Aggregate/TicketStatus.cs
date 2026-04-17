using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Aggregate;

public class TicketStatus
{
    public TicketStatusId Id { get; private set; }
    public TicketStatusName Name { get; private set; }

    private TicketStatus(TicketStatusId id, TicketStatusName name)
    {
        Id = id;
        Name = name;
    }

    public static TicketStatus Create(TicketStatusId id, TicketStatusName name)
    {
        return new TicketStatus(id, name);
    }
}
