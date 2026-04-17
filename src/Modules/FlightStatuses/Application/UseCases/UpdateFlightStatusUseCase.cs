using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;

public class UpdateFlightStatusUseCase
{
    private readonly IFlightStatusRepository _repository;

    public UpdateFlightStatusUseCase(IFlightStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(FlightStatusId id, FlightStatusName name)
    {
        var flightStatus = FlightStatus.Reconstitute(id, name);
        await _repository.UpdateAsync(flightStatus);
    }
}