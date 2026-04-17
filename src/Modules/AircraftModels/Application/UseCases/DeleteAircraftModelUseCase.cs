using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Application.UseCases;

public class DeleteAircraftModelUseCase
{
    private readonly IAircraftModelRepository _repository;

    public DeleteAircraftModelUseCase(IAircraftModelRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(AircraftModelId id)
    {
        await _repository.DeleteAsync(id);
    }
}