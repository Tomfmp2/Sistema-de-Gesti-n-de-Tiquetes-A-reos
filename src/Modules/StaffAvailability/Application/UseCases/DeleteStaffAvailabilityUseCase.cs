using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Application.UseCases;

public class DeleteStaffAvailabilityUseCase
{
    private readonly IStaffAvailabilityRepository _repository;

    public DeleteStaffAvailabilityUseCase(IStaffAvailabilityRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(StaffAvailabilityId id)
    {
        await _repository.DeleteAsync(id);
    }
}