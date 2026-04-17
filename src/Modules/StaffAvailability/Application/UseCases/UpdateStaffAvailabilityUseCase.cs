using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Application.UseCases;

public class UpdateStaffAvailabilityUseCase
{
    private readonly IStaffAvailabilityRepository _repository;

    public UpdateStaffAvailabilityUseCase(IStaffAvailabilityRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        StaffAvailabilityId id,
        StaffId staffId,
        AvailabilityStatusId availabilityStatusId,
        StartDate startDate,
        EndDate endDate,
        Observation? observation)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            throw new InvalidOperationException("Staff availability not found.");
        }

        var updated = StaffAvailabilityRecord.Create(id, staffId, availabilityStatusId, startDate, endDate, observation);
        await _repository.UpdateAsync(updated);
    }
}