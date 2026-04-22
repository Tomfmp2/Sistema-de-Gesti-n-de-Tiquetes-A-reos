using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Infrastructure.Entity;

public class TicketStatusEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<TicketEntity> Tickets { get; set; } = new List<TicketEntity>();
}
