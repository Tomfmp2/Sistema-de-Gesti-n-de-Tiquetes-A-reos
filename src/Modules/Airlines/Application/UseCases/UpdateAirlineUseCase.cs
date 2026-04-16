using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Application.UseCases;

public class UpdateAirlineUseCase
{
    private readonly IAirlineRepository _repository;

    public UpdateAirlineUseCase(IAirlineRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(int id, string name, string iataCode, int originCountryId, bool isActive)
    {
        var airline = await _repository.GetByIdAsync(AirlineId.Reconstitute(id));
        if (airline == null) throw new Exception("Airline not found");
        var airlineName = AirlineName.Create(name);
        var iata = IataCode.Create(iataCode);
        airline.Update(airlineName, iata, originCountryId, isActive);
        await _repository.UpdateAsync(airline);
    }
}