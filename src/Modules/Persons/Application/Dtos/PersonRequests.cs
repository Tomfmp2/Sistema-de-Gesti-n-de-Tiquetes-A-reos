namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.Dtos;

public sealed record CreatePersonRequest(
    int DocumentTypeId,
    string DocumentNumber,
    string FirstName,
    string LastName,
    DateTime? BirthDate,
    char? Gender,
    int? DirectionId
);

public sealed record UpdatePersonRequest(
    int Id,
    int DocumentTypeId,
    string DocumentNumber,
    string FirstName,
    string LastName,
    DateTime? BirthDate,
    char? Gender,
    int? DirectionId
);
