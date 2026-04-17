using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.UseCases;

public interface IGetAllCitiesUseCase
{
    Task<IReadOnlyList<City>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllCitiesUseCase : IGetAllCitiesUseCase
{
    private readonly ICityRepository _repository;

    public GetAllCitiesUseCase(ICityRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<City>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
