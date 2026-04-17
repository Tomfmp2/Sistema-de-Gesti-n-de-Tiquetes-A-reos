namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.Dtos;

public sealed record CreateCountryRequest(string Name, string CodeIso, int ContinentId);

public sealed record UpdateCountryRequest(int Id, string Name, string CodeIso, int ContinentId);
