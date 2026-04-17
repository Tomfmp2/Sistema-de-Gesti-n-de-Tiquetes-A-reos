using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.UseCases;

public class UpdateAvailabilityStatusUseCase
{
    private readonly IAvailabilityStatusRepository _repository;

    public UpdateAvailabilityStatusUseCase(IAvailabilityStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(AvailabilityStatusId id, AvailabilityStatusName name)
    {
        var availabilityStatus = await _repository.GetByIdAsync(id);
        if (availabilityStatus == null) throw new KeyNotFoundException("AvailabilityStatus not found");
        availabilityStatus.UpdateName(name);
        await _repository.UpdateAsync(availabilityStatus);
    }
}