using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.Services;

public sealed class CheckinStatusService : ICheckinStatusService
{
    private readonly ICreateCheckinStatusUseCase _create;
    private readonly IGetCheckinStatusByIdUseCase _getById;
    private readonly IGetAllCheckinStatusesUseCase _getAll;
    private readonly IUpdateCheckinStatusUseCase _update;
    private readonly IDeleteCheckinStatusUseCase _delete;

    public CheckinStatusService(
        ICreateCheckinStatusUseCase create,
        IGetCheckinStatusByIdUseCase getById,
        IGetAllCheckinStatusesUseCase getAll,
        IUpdateCheckinStatusUseCase update,
        IDeleteCheckinStatusUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<CheckinStatus> CreateAsync(
        CreateCheckinStatusRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<CheckinStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<CheckinStatus>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateCheckinStatusRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
