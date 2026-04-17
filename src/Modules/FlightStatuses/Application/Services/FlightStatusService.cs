using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Services;

public sealed class FlightStatusService : IFlightStatusService
{
    private readonly ICreateFlightStatusUseCase _create;
    private readonly IGetFlightStatusByIdUseCase _getById;
    private readonly IGetAllFlightStatusesUseCase _getAll;
    private readonly IUpdateFlightStatusUseCase _update;
    private readonly IDeleteFlightStatusUseCase _delete;

    public FlightStatusService(
        ICreateFlightStatusUseCase create,
        IGetFlightStatusByIdUseCase getById,
        IGetAllFlightStatusesUseCase getAll,
        IUpdateFlightStatusUseCase update,
        IDeleteFlightStatusUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<FlightStatus> CreateAsync(
        CreateFlightStatusRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<FlightStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<FlightStatus>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateFlightStatusRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
