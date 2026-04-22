using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Application.UseCases;

public class CreateStaffAvailabilityUseCase
{
    private readonly IStaffAvailabilityRepository _repository;

    public CreateStaffAvailabilityUseCase(IStaffAvailabilityRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        StaffId staffId,
        AvailabilityStatusId availabilityStatusId,
        StartDate startDate,
        EndDate endDate,
        Observation? observation)
    {
        var staffAvailability = StaffAvailabilityRecord.Create(
            StaffAvailabilityId.Create(0),
            staffId,
            availabilityStatusId,
            startDate,
            endDate,
            observation);
        await _repository.AddAsync(staffAvailability);
    }
}