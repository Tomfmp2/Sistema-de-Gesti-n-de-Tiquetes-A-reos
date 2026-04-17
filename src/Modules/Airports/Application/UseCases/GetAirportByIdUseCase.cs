using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Application.UseCases;

public class GetAirportByIdUseCase
{
    private readonly IAirportRepository _repository;

    public GetAirportByIdUseCase(IAirportRepository repository)
    {
        _repository = repository;
    }

    public async Task<Airport?> ExecuteAsync(int id)
    {
        var airportId = AirportId.Reconstitute(id);
        return await _repository.GetByIdAsync(airportId);
    }
}