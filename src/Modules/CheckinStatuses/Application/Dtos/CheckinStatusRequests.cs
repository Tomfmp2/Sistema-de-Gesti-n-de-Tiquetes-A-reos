namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.Dtos;

public sealed record CreateCheckinStatusRequest(string? Name);

public sealed record UpdateCheckinStatusRequest(int Id, string? Name);
