using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;

public interface ICreateFlightStatusUseCase
{
    Task<FlightStatus> ExecuteAsync(
        CreateFlightStatusRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateFlightStatusUseCase : ICreateFlightStatusUseCase
{
    private readonly IFlightStatusRepository _repository;

    public CreateFlightStatusUseCase(IFlightStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<FlightStatus> ExecuteAsync(
        CreateFlightStatusRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = FlightStatus.Create(new FlightStatusId(0), FlightStatusName.Create(request.Name));
        return _repository.AddAsync(x, cancellationToken);
    }
}
