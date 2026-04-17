namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.Dtos;

public sealed record CreateEmailDomainRequest(string Domain);

public sealed record UpdateEmailDomainRequest(int Id, string Domain);
