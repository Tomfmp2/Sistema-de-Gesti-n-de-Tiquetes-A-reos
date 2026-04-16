namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.Dtos;

public sealed record CreateRegionRequest(string Name, string Type, int CountryId);

public sealed record UpdateRegionRequest(int Id, string Name, string Type, int CountryId);
