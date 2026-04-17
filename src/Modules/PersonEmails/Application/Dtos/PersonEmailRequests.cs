namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.Dtos;

public sealed record CreatePersonEmailRequest(
    int PersonId,
    string EmailLocalPart,
    int EmailDomainId,
    bool IsPrimary
);

public sealed record UpdatePersonEmailRequest(
    int Id,
    int PersonId,
    string EmailLocalPart,
    int EmailDomainId,
    bool IsPrimary
);
