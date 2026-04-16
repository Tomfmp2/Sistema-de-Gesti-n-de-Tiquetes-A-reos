using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Application.UseCases;

public class UpdateAircraftUseCase
{
    private readonly IAircraftRepository _repository;

    public UpdateAircraftUseCase(IAircraftRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        AircraftId id,
        ModelId modelId,
        AirlineId airlineId,
        Registration registration,
        ManufacturingDate? manufacturingDate,
        IsActive isActive)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            throw new InvalidOperationException("Aircraft not found.");
        }

        var updated = Aggregate.Aircraft.Reconstitute(id, modelId, airlineId, registration, manufacturingDate, isActive);
        await _repository.UpdateAsync(updated);
    }
}