namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Application.Dtos;

public sealed record CreateDocumentTypeRequest(string Name, string Code);

public sealed record UpdateDocumentTypeRequest(int Id, string Name, string Code);
