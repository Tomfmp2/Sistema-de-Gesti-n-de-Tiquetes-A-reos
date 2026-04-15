using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.UseCases;

public interface IGetDirectionByIdUseCase
{
    Task<Direction?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetDirectionByIdUseCase : IGetDirectionByIdUseCase
{
    private readonly IDirectionRepository _repository;

    public GetDirectionByIdUseCase(IDirectionRepository repository)
    {
        _repository = repository;
    }

    public Task<Direction?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Direction?>(null);
        }

        return _repository.GetByIdAsync(DirectionId.Create(id), cancellationToken);
    }
}
