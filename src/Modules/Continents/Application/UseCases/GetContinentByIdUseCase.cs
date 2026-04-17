using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.UseCases;

public interface IGetContinentByIdUseCase
{
    Task<Continent?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetContinentByIdUseCase : IGetContinentByIdUseCase
{
    private readonly IContinentRepository _repository;

    public GetContinentByIdUseCase(IContinentRepository repository)
    {
        _repository = repository;
    }

    public Task<Continent?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Continent?>(null);
        }

        return _repository.GetByIdAsync(ContinentId.Create(id), cancellationToken);
    }
}
