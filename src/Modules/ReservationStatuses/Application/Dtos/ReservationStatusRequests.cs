namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.Dtos;

public sealed record CreateReservationStatusRequest(string? Name);

public sealed record UpdateReservationStatusRequest(int Id, string? Name);
