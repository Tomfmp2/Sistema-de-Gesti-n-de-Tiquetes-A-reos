namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.Dtos;

public sealed record CreateTicketRequest(int ReservationPassengerId, string? Code, DateTime IssueDate, int TicketStatusId, DateTime CreatedAt, DateTime UpdatedAt);

public sealed record UpdateTicketRequest(int Id, int ReservationPassengerId, string? Code, DateTime IssueDate, int TicketStatusId, DateTime CreatedAt, DateTime UpdatedAt);
