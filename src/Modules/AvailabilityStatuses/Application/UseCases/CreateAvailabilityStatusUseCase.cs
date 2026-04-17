using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.UseCases;

public class CreateAvailabilityStatusUseCase
{
    private readonly IAvailabilityStatusRepository _repository;

    public CreateAvailabilityStatusUseCase(IAvailabilityStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(AvailabilityStatusName name)
    {
        // Generate new ID (in real app, use a proper ID generator)
        var id = AvailabilityStatusId.Create(new Random().Next(1, int.MaxValue));
        var availabilityStatus = AvailabilityStatus.Create(id, name);
        await _repository.AddAsync(availabilityStatus);
    }
}