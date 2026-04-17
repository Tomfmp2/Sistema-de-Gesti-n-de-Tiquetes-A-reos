using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Application.UseCases;

public class GetAirportAirlineByIdUseCase
{
    private readonly IAirportAirlineRepository _repository;

    public GetAirportAirlineByIdUseCase(IAirportAirlineRepository repository)
    {
        _repository = repository;
    }

    public async Task<AirportAirlineRecord?> ExecuteAsync(int id)
    {
        var airportAirlineId = AirportAirlineId.Reconstitute(id);
        return await _repository.GetByIdAsync(airportAirlineId);
    }
}