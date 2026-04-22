using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Infrastructure.Data;

public static class TicketStatusDefaultData
{
    public static readonly TicketStatusEntity[] TicketStatuses =
    [
        new() { Id = 1, Name = "Emitido" },
        new() { Id = 2, Name = "Usado" },
        new() { Id = 3, Name = "Cancelado" },
        new() { Id = 4, Name = "Reembolsado" },
        new() { Id = 5, Name = "No presentado" }
    ];
}
