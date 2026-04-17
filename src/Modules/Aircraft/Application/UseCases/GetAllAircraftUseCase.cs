using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Application.UseCases;

public class GetAllAircraftUseCase
{
    private readonly IAircraftRepository _repository;

    public GetAllAircraftUseCase(IAircraftRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Aggregate.Aircraft>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}