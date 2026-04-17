using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.UseCases;

public interface IUpdateFlightStatusTransitionUseCase
{
    Task ExecuteAsync(
        UpdateFlightStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateFlightStatusTransitionUseCase : IUpdateFlightStatusTransitionUseCase
{
    private readonly IFlightStatusTransitionRepository _repository;

    public UpdateFlightStatusTransitionUseCase(IFlightStatusTransitionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateFlightStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = FlightStatusTransition.Create(FlightStatusTransitionId.Create(request.Id), FlightStatusTransitionOriginStatusId.Create(request.OriginStatusId), FlightStatusTransitionDestinationStatusId.Create(request.DestinationStatusId));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
