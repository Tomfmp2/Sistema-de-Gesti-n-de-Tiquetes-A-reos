using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.UseCases;

public interface IUpdatePersonUseCase
{
    Task ExecuteAsync(
        UpdatePersonRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdatePersonUseCase : IUpdatePersonUseCase
{
    private readonly IPersonRepository _repository;

    public UpdatePersonUseCase(IPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        UpdatePersonRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var existing = await _repository.GetByIdAsync(PersonId.Create(request.Id), cancellationToken);
        if (existing is null)
        {
            throw new InvalidOperationException($"No existe persona {request.Id}.");
        }

        var x = Person.Create(
            PersonId.Create(request.Id),
            PersonDocumentTypeRefId.Create(request.DocumentTypeId),
            PersonDocumentNumber.Create(request.DocumentNumber),
            PersonFirstName.Create(request.FirstName),
            PersonLastName.Create(request.LastName),
            request.BirthDate,
            request.Gender,
            request.DirectionId,
            existing.CreatedAt,
            existing.UpdatedAt
        );
        await _repository.UpdateAsync(x, cancellationToken);
    }
}
