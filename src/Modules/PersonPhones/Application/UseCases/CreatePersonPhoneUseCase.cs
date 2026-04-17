using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.UseCases;

public interface ICreatePersonPhoneUseCase
{
    Task<PersonPhone> ExecuteAsync(
        CreatePersonPhoneRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreatePersonPhoneUseCase : ICreatePersonPhoneUseCase
{
    private readonly IPersonPhoneRepository _repository;

    public CreatePersonPhoneUseCase(IPersonPhoneRepository repository)
    {
        _repository = repository;
    }

    public Task<PersonPhone> ExecuteAsync(
        CreatePersonPhoneRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = PersonPhone.CreateNew(
            PersonPhonePersonId.Create(request.PersonId),
            PersonPhoneCodeRefId.Create(request.PhoneCodeId),
            PersonPhoneLineNumber.Create(request.Number),
            request.IsPrimary
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}
