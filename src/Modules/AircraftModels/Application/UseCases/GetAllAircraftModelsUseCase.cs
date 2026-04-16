using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Application.UseCases;

public class GetAllAircraftModelsUseCase
{
    private readonly IAircraftModelRepository _repository;

    public GetAllAircraftModelsUseCase(IAircraftModelRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AircraftModel>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}