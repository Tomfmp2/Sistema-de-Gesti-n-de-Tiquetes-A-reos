using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.UseCases;

public interface ICreateFlightStatusTransitionUseCase
{
    Task<FlightStatusTransition> ExecuteAsync(
        CreateFlightStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateFlightStatusTransitionUseCase : ICreateFlightStatusTransitionUseCase
{
    private readonly IFlightStatusTransitionRepository _repository;

    public CreateFlightStatusTransitionUseCase(IFlightStatusTransitionRepository repository)
    {
        _repository = repository;
    }

    public Task<FlightStatusTransition> ExecuteAsync(
        CreateFlightStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = FlightStatusTransition.Create(new FlightStatusTransitionId(0), FlightStatusTransitionOriginStatusId.Create(request.OriginStatusId), FlightStatusTransitionDestinationStatusId.Create(request.DestinationStatusId));
        return _repository.AddAsync(x, cancellationToken);
    }
}
