namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.Dtos;

public sealed record CreateTicketStatusRequest(string? Name);

public sealed record UpdateTicketStatusRequest(int Id, string? Name);
