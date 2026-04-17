using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Application.UseCases;

public class DeleteAirlineUseCase
{
    private readonly IAirlineRepository _repository;

    public DeleteAirlineUseCase(IAirlineRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(int id)
    {
        var airlineId = AirlineId.Reconstitute(id);
        await _repository.DeleteAsync(airlineId);
    }
}