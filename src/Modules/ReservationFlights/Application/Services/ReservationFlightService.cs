using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.Services;

public sealed class ReservationFlightService : IReservationFlightService
{
    private readonly ICreateReservationFlightUseCase _create;
    private readonly IGetReservationFlightByIdUseCase _getById;
    private readonly IGetAllReservationFlightsUseCase _getAll;
    private readonly IUpdateReservationFlightUseCase _update;
    private readonly IDeleteReservationFlightUseCase _delete;

    public ReservationFlightService(
        ICreateReservationFlightUseCase create,
        IGetReservationFlightByIdUseCase getById,
        IGetAllReservationFlightsUseCase getAll,
        IUpdateReservationFlightUseCase update,
        IDeleteReservationFlightUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<ReservationFlight> CreateAsync(
        CreateReservationFlightRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<ReservationFlight?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<ReservationFlight>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateReservationFlightRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
