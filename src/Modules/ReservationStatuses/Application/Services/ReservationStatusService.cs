using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.Services;

public sealed class ReservationStatusService : IReservationStatusService
{
    private readonly ICreateReservationStatusUseCase _create;
    private readonly IGetReservationStatusByIdUseCase _getById;
    private readonly IGetAllReservationStatusesUseCase _getAll;
    private readonly IUpdateReservationStatusUseCase _update;
    private readonly IDeleteReservationStatusUseCase _delete;

    public ReservationStatusService(
        ICreateReservationStatusUseCase create,
        IGetReservationStatusByIdUseCase getById,
        IGetAllReservationStatusesUseCase getAll,
        IUpdateReservationStatusUseCase update,
        IDeleteReservationStatusUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<ReservationStatus> CreateAsync(
        CreateReservationStatusRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<ReservationStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<ReservationStatus>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateReservationStatusRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
