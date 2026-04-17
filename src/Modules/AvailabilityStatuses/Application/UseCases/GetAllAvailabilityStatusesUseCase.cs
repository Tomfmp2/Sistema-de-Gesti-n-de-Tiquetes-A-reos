using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.UseCases;

public class GetAllAvailabilityStatusesUseCase
{
    private readonly IAvailabilityStatusRepository _repository;

    public GetAllAvailabilityStatusesUseCase(IAvailabilityStatusRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AvailabilityStatus>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}