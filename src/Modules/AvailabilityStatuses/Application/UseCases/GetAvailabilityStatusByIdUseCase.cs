using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.UseCases;

public class GetAvailabilityStatusByIdUseCase
{
    private readonly IAvailabilityStatusRepository _repository;

    public GetAvailabilityStatusByIdUseCase(IAvailabilityStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<AvailabilityStatus?> ExecuteAsync(AvailabilityStatusId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}