using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.UseCases;

public interface IUpdatePersonEmailUseCase
{
    Task ExecuteAsync(
        UpdatePersonEmailRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdatePersonEmailUseCase : IUpdatePersonEmailUseCase
{
    private readonly IPersonEmailRepository _repository;

    public UpdatePersonEmailUseCase(IPersonEmailRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdatePersonEmailRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = PersonEmail.Create(
            PersonEmailId.Create(request.Id),
            PersonEmailPersonId.Create(request.PersonId),
            PersonEmailLocalPart.Create(request.EmailLocalPart),
            PersonEmailDomainRefId.Create(request.EmailDomainId),
            request.IsPrimary
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
