using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.UseCases;

public interface IGetAllPersonEmailsUseCase
{
    Task<IReadOnlyList<PersonEmail>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllPersonEmailsUseCase : IGetAllPersonEmailsUseCase
{
    private readonly IPersonEmailRepository _repository;

    public GetAllPersonEmailsUseCase(IPersonEmailRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<PersonEmail>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
