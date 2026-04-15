using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.Services;

public sealed class DirectionService : IDirectionService
{
    private readonly ICreateDirectionUseCase _create;
    private readonly IGetDirectionByIdUseCase _getById;
    private readonly IGetAllDirectionsUseCase _getAll;
    private readonly IUpdateDirectionUseCase _update;
    private readonly IDeleteDirectionUseCase _delete;

    public DirectionService(
        ICreateDirectionUseCase create,
        IGetDirectionByIdUseCase getById,
        IGetAllDirectionsUseCase getAll,
        IUpdateDirectionUseCase update,
        IDeleteDirectionUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Direction> CreateAsync(
        CreateDirectionRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Direction?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Direction>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateDirectionRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
