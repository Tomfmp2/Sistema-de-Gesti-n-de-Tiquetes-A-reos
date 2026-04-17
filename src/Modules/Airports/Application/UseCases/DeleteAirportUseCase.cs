using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Application.UseCases;

public class DeleteAirportUseCase
{
    private readonly IAirportRepository _repository;

    public DeleteAirportUseCase(IAirportRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(int id)
    {
        var airportId = AirportId.Reconstitute(id);
        await _repository.DeleteAsync(airportId);
    }
}