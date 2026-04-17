namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.Dtos;

public sealed record CreatePersonPhoneRequest(
    int PersonId,
    int PhoneCodeId,
    string Number,
    bool IsPrimary
);

public sealed record UpdatePersonPhoneRequest(
    int Id,
    int PersonId,
    int PhoneCodeId,
    string Number,
    bool IsPrimary
);
