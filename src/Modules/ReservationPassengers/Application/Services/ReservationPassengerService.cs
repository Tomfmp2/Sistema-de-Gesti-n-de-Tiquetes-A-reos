using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.Services;

public sealed class ReservationPassengerService : IReservationPassengerService
{
    private readonly ICreateReservationPassengerUseCase _create;
    private readonly IGetReservationPassengerByIdUseCase _getById;
    private readonly IGetAllReservationPassengersUseCase _getAll;
    private readonly IUpdateReservationPassengerUseCase _update;
    private readonly IDeleteReservationPassengerUseCase _delete;

    public ReservationPassengerService(
        ICreateReservationPassengerUseCase create,
        IGetReservationPassengerByIdUseCase getById,
        IGetAllReservationPassengersUseCase getAll,
        IUpdateReservationPassengerUseCase update,
        IDeleteReservationPassengerUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<ReservationPassenger> CreateAsync(
        CreateReservationPassengerRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<ReservationPassenger?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<ReservationPassenger>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateReservationPassengerRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
