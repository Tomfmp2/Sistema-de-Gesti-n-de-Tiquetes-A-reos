using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Application.UseCases;

public class GetAllAirportsUseCase
{
    private readonly IAirportRepository _repository;

    public GetAllAirportsUseCase(IAirportRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Airport>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}