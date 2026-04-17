using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Application.UseCases;

public class GetAllAirportAirlinesUseCase
{
    private readonly IAirportAirlineRepository _repository;

    public GetAllAirportAirlinesUseCase(IAirportAirlineRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AirportAirlineRecord>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}