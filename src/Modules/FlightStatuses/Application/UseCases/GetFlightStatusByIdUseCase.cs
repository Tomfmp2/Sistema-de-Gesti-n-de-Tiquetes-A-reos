using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;

public class GetFlightStatusByIdUseCase
{
    private readonly IFlightStatusRepository _repository;

    public GetFlightStatusByIdUseCase(IFlightStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<FlightStatus> ExecuteAsync(FlightStatusId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}