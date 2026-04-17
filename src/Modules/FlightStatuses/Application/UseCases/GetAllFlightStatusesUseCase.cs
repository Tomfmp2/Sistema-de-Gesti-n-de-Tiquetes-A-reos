using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;

public class GetAllFlightStatusesUseCase
{
    private readonly IFlightStatusRepository _repository;

    public GetAllFlightStatusesUseCase(IFlightStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FlightStatus>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}