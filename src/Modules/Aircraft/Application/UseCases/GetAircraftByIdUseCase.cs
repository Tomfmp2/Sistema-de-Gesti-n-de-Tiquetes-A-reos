using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Application.UseCases;

public class GetAircraftByIdUseCase
{
    private readonly IAircraftRepository _repository;

    public GetAircraftByIdUseCase(IAircraftRepository repository)
    {
        _repository = repository;
    }

    public async Task<Aggregate.Aircraft?> ExecuteAsync(AircraftId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}