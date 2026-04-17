namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Dtos;

public sealed record CreateFlightStatusRequest(string? Name);

public sealed record UpdateFlightStatusRequest(int Id, string? Name);
