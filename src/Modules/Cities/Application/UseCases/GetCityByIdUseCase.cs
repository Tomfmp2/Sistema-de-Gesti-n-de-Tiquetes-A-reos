using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.UseCases;

public interface IGetCityByIdUseCase
{
    Task<City?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetCityByIdUseCase : IGetCityByIdUseCase
{
    private readonly ICityRepository _repository;

    public GetCityByIdUseCase(ICityRepository repository)
    {
        _repository = repository;
    }

    public Task<City?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<City?>(null);
        }

        return _repository.GetByIdAsync(CityId.Create(id), cancellationToken);
    }
}
