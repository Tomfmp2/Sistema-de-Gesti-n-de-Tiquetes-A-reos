using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.Services;

public sealed class ReservationStatusTransitionService : IReservationStatusTransitionService
{
    private readonly ICreateReservationStatusTransitionUseCase _create;
    private readonly IGetReservationStatusTransitionByIdUseCase _getById;
    private readonly IGetAllReservationStatusTransitionsUseCase _getAll;
    private readonly IUpdateReservationStatusTransitionUseCase _update;
    private readonly IDeleteReservationStatusTransitionUseCase _delete;

    public ReservationStatusTransitionService(
        ICreateReservationStatusTransitionUseCase create,
        IGetReservationStatusTransitionByIdUseCase getById,
        IGetAllReservationStatusTransitionsUseCase getAll,
        IUpdateReservationStatusTransitionUseCase update,
        IDeleteReservationStatusTransitionUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<ReservationStatusTransition> CreateAsync(
        CreateReservationStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<ReservationStatusTransition?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<ReservationStatusTransition>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateReservationStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
