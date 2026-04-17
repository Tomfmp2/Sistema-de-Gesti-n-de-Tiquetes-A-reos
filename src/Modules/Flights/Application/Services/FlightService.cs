using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.Services;

public sealed class FlightService : IFlightService
{
    private readonly ICreateFlightUseCase _create;
    private readonly IGetFlightByIdUseCase _getById;
    private readonly IGetAllFlightsUseCase _getAll;
    private readonly IUpdateFlightUseCase _update;
    private readonly IDeleteFlightUseCase _delete;

    public FlightService(
        ICreateFlightUseCase create,
        IGetFlightByIdUseCase getById,
        IGetAllFlightsUseCase getAll,
        IUpdateFlightUseCase update,
        IDeleteFlightUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Flight> CreateAsync(
        CreateFlightRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Flight?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Flight>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateFlightRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
