using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;

public class CreateFlightStatusUseCase
{
    private readonly IFlightStatusRepository _repository;

    public CreateFlightStatusUseCase(IFlightStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(FlightStatusName name)
    {
        var flightStatus = FlightStatus.Create(name);
        await _repository.AddAsync(flightStatus);
    }
}