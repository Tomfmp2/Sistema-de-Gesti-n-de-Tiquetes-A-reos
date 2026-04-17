using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.UseCases;

public interface IGetAllFlightStatusTransitionsUseCase
{
    Task<IReadOnlyList<FlightStatusTransition>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllFlightStatusTransitionsUseCase : IGetAllFlightStatusTransitionsUseCase
{
    private readonly IFlightStatusTransitionRepository _repository;

    public GetAllFlightStatusTransitionsUseCase(IFlightStatusTransitionRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<FlightStatusTransition>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
