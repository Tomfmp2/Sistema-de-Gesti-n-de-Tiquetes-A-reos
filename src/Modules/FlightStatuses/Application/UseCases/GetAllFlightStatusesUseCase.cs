using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;

public interface IGetAllFlightStatusesUseCase
{
    Task<IReadOnlyList<FlightStatus>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllFlightStatusesUseCase : IGetAllFlightStatusesUseCase
{
    private readonly IFlightStatusRepository _repository;

    public GetAllFlightStatusesUseCase(IFlightStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<FlightStatus>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
