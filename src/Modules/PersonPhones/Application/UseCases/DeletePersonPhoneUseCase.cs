using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.UseCases;

public interface IDeletePersonPhoneUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeletePersonPhoneUseCase : IDeletePersonPhoneUseCase
{
    private readonly IPersonPhoneRepository _repository;

    public DeletePersonPhoneUseCase(IPersonPhoneRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(PersonPhoneId.Create(id), cancellationToken);
    }
}
