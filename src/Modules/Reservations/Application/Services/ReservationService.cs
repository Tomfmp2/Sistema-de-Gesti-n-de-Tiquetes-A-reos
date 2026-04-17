using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Services;

public sealed class ReservationService : IReservationService
{
    private readonly ICreateReservationUseCase _create;
    private readonly IGetReservationByIdUseCase _getById;
    private readonly IGetAllReservationsUseCase _getAll;
    private readonly IUpdateReservationUseCase _update;
    private readonly IDeleteReservationUseCase _delete;

    public ReservationService(
        ICreateReservationUseCase create,
        IGetReservationByIdUseCase getById,
        IGetAllReservationsUseCase getAll,
        IUpdateReservationUseCase update,
        IDeleteReservationUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Reservation> CreateAsync(
        CreateReservationRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Reservation?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Reservation>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateReservationRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
