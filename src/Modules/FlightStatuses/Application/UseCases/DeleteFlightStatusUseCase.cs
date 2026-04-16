using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;

public class DeleteFlightStatusUseCase
{
    private readonly IFlightStatusRepository _repository;

    public DeleteFlightStatusUseCase(IFlightStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(FlightStatusId id)
    {
        await _repository.DeleteAsync(id);
    }
}