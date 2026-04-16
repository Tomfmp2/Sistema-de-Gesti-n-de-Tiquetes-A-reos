using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.UseCases;

public interface IGetAllContinentsUseCase
{
    Task<IReadOnlyList<Continent>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllContinentsUseCase : IGetAllContinentsUseCase
{
    private readonly IContinentRepository _repository;

    public GetAllContinentsUseCase(IContinentRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Continent>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
