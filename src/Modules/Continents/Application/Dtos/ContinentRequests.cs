namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.Dtos;

public sealed record CreateContinentRequest(string Name);

public sealed record UpdateContinentRequest(int Id, string Name);
