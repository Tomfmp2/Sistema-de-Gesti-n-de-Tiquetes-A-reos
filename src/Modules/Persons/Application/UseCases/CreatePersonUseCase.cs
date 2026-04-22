using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.UseCases;

public interface ICreatePersonUseCase
{
    Task<Person> ExecuteAsync(
        CreatePersonRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreatePersonUseCase : ICreatePersonUseCase
{
    private readonly IPersonRepository _repository;

    public CreatePersonUseCase(IPersonRepository repository)
    {
        _repository = repository;
    }

    public Task<Person> ExecuteAsync(
        CreatePersonRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Person.CreateNew(
            PersonDocumentTypeRefId.Create(request.DocumentTypeId),
            PersonDocumentNumber.Create(request.DocumentNumber),
            PersonFirstName.Create(request.FirstName),
            PersonLastName.Create(request.LastName),
            request.BirthDate,
            request.Gender,
            request.AddressId
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}
