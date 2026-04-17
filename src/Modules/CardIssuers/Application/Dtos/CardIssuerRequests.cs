namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.Dtos;

public sealed record CreateCardIssuerRequest(string? Name);

public sealed record UpdateCardIssuerRequest(int Id, string? Name);
