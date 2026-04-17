using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.UseCases;

public class DeleteAvailabilityStatusUseCase
{
    private readonly IAvailabilityStatusRepository _repository;

    public DeleteAvailabilityStatusUseCase(IAvailabilityStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(AvailabilityStatusId id)
    {
        await _repository.DeleteAsync(id);
    }
}