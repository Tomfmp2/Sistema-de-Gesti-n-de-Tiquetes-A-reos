using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.UseCases;

public interface IGetAllPersonsUseCase
{
    Task<IReadOnlyList<Person>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllPersonsUseCase : IGetAllPersonsUseCase
{
    private readonly IPersonRepository _repository;

    public GetAllPersonsUseCase(IPersonRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Person>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
