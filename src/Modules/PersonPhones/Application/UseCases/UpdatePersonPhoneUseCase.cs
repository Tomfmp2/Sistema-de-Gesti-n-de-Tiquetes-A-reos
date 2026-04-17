using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.UseCases;

public interface IUpdatePersonPhoneUseCase
{
    Task ExecuteAsync(
        UpdatePersonPhoneRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdatePersonPhoneUseCase : IUpdatePersonPhoneUseCase
{
    private readonly IPersonPhoneRepository _repository;

    public UpdatePersonPhoneUseCase(IPersonPhoneRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdatePersonPhoneRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = PersonPhone.Create(
            PersonPhoneId.Create(request.Id),
            PersonPhonePersonId.Create(request.PersonId),
            PersonPhoneCodeRefId.Create(request.PhoneCodeId),
            PersonPhoneLineNumber.Create(request.Number),
            request.IsPrimary
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
