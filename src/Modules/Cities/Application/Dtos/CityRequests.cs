namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.Dtos;

public sealed record CreateCityRequest(string Name, int RegionId);

public sealed record UpdateCityRequest(int Id, string Name, int RegionId);
