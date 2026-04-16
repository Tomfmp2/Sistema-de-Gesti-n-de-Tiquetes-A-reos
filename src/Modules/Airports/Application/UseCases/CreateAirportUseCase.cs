using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Application.UseCases;

public class CreateAirportUseCase
{
    private readonly IAirportRepository _repository;

    public CreateAirportUseCase(IAirportRepository repository)
    {
        _repository = repository;
    }

    public async Task<Airport> ExecuteAsync(string name, string iataCode, string? icaoCode, int cityId)
    {
        var airportName = AirportName.Create(name);
        var iata = IataCode.Create(iataCode);
        var icao = IcaoCode.Create(icaoCode);
        var airport = Airport.Create(airportName, iata, icao, cityId);
        await _repository.AddAsync(airport);
        return airport;
    }
}