using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.UseCases;

public interface IDeletePersonUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeletePersonUseCase : IDeletePersonUseCase
{
    private readonly IPersonRepository _repository;

    public DeletePersonUseCase(IPersonRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(PersonId.Create(id), cancellationToken);
    }
}
