using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.UseCases;

public interface IGetPersonByIdUseCase
{
    Task<Person?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetPersonByIdUseCase : IGetPersonByIdUseCase
{
    private readonly IPersonRepository _repository;

    public GetPersonByIdUseCase(IPersonRepository repository)
    {
        _repository = repository;
    }

    public Task<Person?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Person?>(null);
        }

        return _repository.GetByIdAsync(PersonId.Create(id), cancellationToken);
    }
}
