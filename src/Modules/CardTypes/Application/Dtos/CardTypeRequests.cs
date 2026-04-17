namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.Dtos;

public sealed record CreateCardTypeRequest(string? Name);

public sealed record UpdateCardTypeRequest(int Id, string? Name);
