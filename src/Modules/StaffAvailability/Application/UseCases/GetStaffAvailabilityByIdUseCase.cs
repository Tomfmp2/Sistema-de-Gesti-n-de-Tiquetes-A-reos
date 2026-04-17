using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Application.UseCases;

public class GetStaffAvailabilityByIdUseCase
{
    private readonly IStaffAvailabilityRepository _repository;

    public GetStaffAvailabilityByIdUseCase(IStaffAvailabilityRepository repository)
    {
        _repository = repository;
    }

    public async Task<StaffAvailabilityRecord?> ExecuteAsync(StaffAvailabilityId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}