using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.UseCases;

public interface IGetFlightStatusTransitionByIdUseCase
{
    Task<FlightStatusTransition?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetFlightStatusTransitionByIdUseCase : IGetFlightStatusTransitionByIdUseCase
{
    private readonly IFlightStatusTransitionRepository _repository;

    public GetFlightStatusTransitionByIdUseCase(IFlightStatusTransitionRepository repository)
    {
        _repository = repository;
    }

    public Task<FlightStatusTransition?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<FlightStatusTransition?>(null);
        }

        return _repository.GetByIdAsync(FlightStatusTransitionId.Create(id), cancellationToken);
    }
}
