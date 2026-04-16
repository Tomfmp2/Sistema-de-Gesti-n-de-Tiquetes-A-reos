using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Application.UseCases;

public class CreateAirlineUseCase
{
    private readonly IAirlineRepository _repository;

    public CreateAirlineUseCase(IAirlineRepository repository)
    {
        _repository = repository;
    }

    public async Task<Airline> ExecuteAsync(string name, string iataCode, int originCountryId)
    {
        var airlineName = AirlineName.Create(name);
        var iata = IataCode.Create(iataCode);
        var airline = Airline.Create(airlineName, iata, originCountryId);
        await _repository.AddAsync(airline);
        return airline;
    }
}