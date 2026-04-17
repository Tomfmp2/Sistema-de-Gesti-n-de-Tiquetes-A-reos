using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Application.UseCases;

public class DeleteAircraftUseCase
{
    private readonly IAircraftRepository _repository;

    public DeleteAircraftUseCase(IAircraftRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(AircraftId id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            throw new InvalidOperationException("Aircraft not found.");
        }

        await _repository.DeleteAsync(id);
    }
}