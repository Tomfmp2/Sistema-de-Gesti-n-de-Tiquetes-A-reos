using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;

public interface IUpdateFlightStatusUseCase
{
    Task ExecuteAsync(
        UpdateFlightStatusRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateFlightStatusUseCase : IUpdateFlightStatusUseCase
{
    private readonly IFlightStatusRepository _repository;

    public UpdateFlightStatusUseCase(IFlightStatusRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateFlightStatusRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = FlightStatus.Create(FlightStatusId.Create(request.Id), FlightStatusName.Create(request.Name));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
