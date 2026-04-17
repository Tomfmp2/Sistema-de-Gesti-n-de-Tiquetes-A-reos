using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Application.UseCases;

public class GetAllStaffAvailabilitiesUseCase
{
    private readonly IStaffAvailabilityRepository _repository;

    public GetAllStaffAvailabilitiesUseCase(IStaffAvailabilityRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StaffAvailabilityRecord>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}