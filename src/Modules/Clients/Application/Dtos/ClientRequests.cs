namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.Dtos;

public sealed record CreateClientRequest(int PersonId);

public sealed record UpdateClientRequest(int Id, int PersonId);
