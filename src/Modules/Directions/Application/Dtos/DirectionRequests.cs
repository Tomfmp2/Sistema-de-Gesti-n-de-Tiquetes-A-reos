namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.Dtos;

public sealed record CreateDirectionRequest(
    int CityId,
    int StreetTypeId,
    string StreetName,
    string Number,
    string? Complement,
    string? PostalCode
);

public sealed record UpdateDirectionRequest(
    int Id,
    int CityId,
    int StreetTypeId,
    string StreetName,
    string Number,
    string? Complement,
    string? PostalCode
);
