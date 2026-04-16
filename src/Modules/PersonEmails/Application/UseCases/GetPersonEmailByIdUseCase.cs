using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.UseCases;

public interface IGetPersonEmailByIdUseCase
{
    Task<PersonEmail?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetPersonEmailByIdUseCase : IGetPersonEmailByIdUseCase
{
    private readonly IPersonEmailRepository _repository;

    public GetPersonEmailByIdUseCase(IPersonEmailRepository repository)
    {
        _repository = repository;
    }

    public Task<PersonEmail?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<PersonEmail?>(null);
        }

        return _repository.GetByIdAsync(PersonEmailId.Create(id), cancellationToken);
    }
}
