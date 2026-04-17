using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Application.UseCases;

public class DeleteAirportAirlineUseCase
{
    private readonly IAirportAirlineRepository _repository;

    public DeleteAirportAirlineUseCase(IAirportAirlineRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(int id)
    {
        var airportAirlineId = AirportAirlineId.Reconstitute(id);
        await _repository.DeleteAsync(airportAirlineId);
    }
}