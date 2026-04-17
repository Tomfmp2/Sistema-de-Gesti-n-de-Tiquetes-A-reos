using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.UseCases;

public interface ICreatePersonEmailUseCase
{
    Task<PersonEmail> ExecuteAsync(
        CreatePersonEmailRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreatePersonEmailUseCase : ICreatePersonEmailUseCase
{
    private readonly IPersonEmailRepository _repository;

    public CreatePersonEmailUseCase(IPersonEmailRepository repository)
    {
        _repository = repository;
    }

    public Task<PersonEmail> ExecuteAsync(
        CreatePersonEmailRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = PersonEmail.CreateNew(
            PersonEmailPersonId.Create(request.PersonId),
            PersonEmailLocalPart.Create(request.EmailLocalPart),
            PersonEmailDomainRefId.Create(request.EmailDomainId),
            request.IsPrimary
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}
