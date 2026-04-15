using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.UseCases;

public interface IGetAllDirectionsUseCase
{
    Task<IReadOnlyList<Direction>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllDirectionsUseCase : IGetAllDirectionsUseCase
{
    private readonly IDirectionRepository _repository;

    public GetAllDirectionsUseCase(IDirectionRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Direction>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
