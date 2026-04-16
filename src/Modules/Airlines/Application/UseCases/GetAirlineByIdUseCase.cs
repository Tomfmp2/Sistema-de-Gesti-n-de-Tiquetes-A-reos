using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Application.UseCases;

public class GetAirlineByIdUseCase
{
    private readonly IAirlineRepository _repository;

    public GetAirlineByIdUseCase(IAirlineRepository repository)
    {
        _repository = repository;
    }

    public async Task<Airline?> ExecuteAsync(int id)
    {
        var airlineId = AirlineId.Reconstitute(id);
        return await _repository.GetByIdAsync(airlineId);
    }
}