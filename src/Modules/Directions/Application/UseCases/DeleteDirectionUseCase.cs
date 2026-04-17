using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.UseCases;

public interface IDeleteDirectionUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteDirectionUseCase : IDeleteDirectionUseCase
{
    private readonly IDirectionRepository _repository;

    public DeleteDirectionUseCase(IDirectionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(DirectionId.Create(id), cancellationToken);
    }
}
