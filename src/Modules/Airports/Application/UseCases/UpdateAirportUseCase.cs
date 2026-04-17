using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Application.UseCases;

public class UpdateAirportUseCase
{
    private readonly IAirportRepository _repository;

    public UpdateAirportUseCase(IAirportRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(int id, string name, string iataCode, string? icaoCode, int cityId)
    {
        var airport = await _repository.GetByIdAsync(AirportId.Reconstitute(id));
        if (airport == null) throw new Exception("Airport not found");
        var airportName = AirportName.Create(name);
        var iata = IataCode.Create(iataCode);
        var icao = IcaoCode.Create(icaoCode);
        airport.Update(airportName, iata, icao, cityId);
        await _repository.UpdateAsync(airport);
    }
}