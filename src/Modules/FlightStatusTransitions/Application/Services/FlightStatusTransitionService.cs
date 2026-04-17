using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.Services;

public sealed class FlightStatusTransitionService : IFlightStatusTransitionService
{
    private readonly ICreateFlightStatusTransitionUseCase _create;
    private readonly IGetFlightStatusTransitionByIdUseCase _getById;
    private readonly IGetAllFlightStatusTransitionsUseCase _getAll;
    private readonly IUpdateFlightStatusTransitionUseCase _update;
    private readonly IDeleteFlightStatusTransitionUseCase _delete;

    public FlightStatusTransitionService(
        ICreateFlightStatusTransitionUseCase create,
        IGetFlightStatusTransitionByIdUseCase getById,
        IGetAllFlightStatusTransitionsUseCase getAll,
        IUpdateFlightStatusTransitionUseCase update,
        IDeleteFlightStatusTransitionUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<FlightStatusTransition> CreateAsync(
        CreateFlightStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<FlightStatusTransition?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<FlightStatusTransition>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateFlightStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
