namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.Dtos;

public sealed record CreateStreetTypeRequest(string Name);

public sealed record UpdateStreetTypeRequest(int Id, string Name);
