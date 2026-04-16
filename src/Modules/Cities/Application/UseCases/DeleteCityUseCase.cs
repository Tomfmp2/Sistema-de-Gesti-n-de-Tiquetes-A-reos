using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.UseCases;

public interface IDeleteCityUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteCityUseCase : IDeleteCityUseCase
{
    private readonly ICityRepository _repository;

    public DeleteCityUseCase(ICityRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(CityId.Create(id), cancellationToken);
    }
}
