using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.UseCases;

public interface IDeleteContinentUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteContinentUseCase : IDeleteContinentUseCase
{
    private readonly IContinentRepository _repository;

    public DeleteContinentUseCase(IContinentRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(ContinentId.Create(id), cancellationToken);
    }
}
