using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Application.UseCases;

public class GetAircraftModelByIdUseCase
{
    private readonly IAircraftModelRepository _repository;

    public GetAircraftModelByIdUseCase(IAircraftModelRepository repository)
    {
        _repository = repository;
    }

    public async Task<AircraftModel?> ExecuteAsync(AircraftModelId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}