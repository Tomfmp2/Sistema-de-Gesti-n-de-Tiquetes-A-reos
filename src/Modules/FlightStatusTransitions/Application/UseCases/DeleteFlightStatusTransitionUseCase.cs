using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.UseCases;

public interface IDeleteFlightStatusTransitionUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteFlightStatusTransitionUseCase : IDeleteFlightStatusTransitionUseCase
{
    private readonly IFlightStatusTransitionRepository _repository;

    public DeleteFlightStatusTransitionUseCase(IFlightStatusTransitionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(FlightStatusTransitionId.Create(id), cancellationToken);
    }
}
