using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.UseCases;

public class UpdateStaffUseCase
{
    private readonly IStaffRepository _repository;

    public UpdateStaffUseCase(IStaffRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        StaffId id,
        PositionId? positionId = null,
        AirlineId? airlineId = null,
        AirportId? airportId = null,
        HireDate? hireDate = null,
        IsActive? isActive = null)
    {
        var staff = await _repository.GetByIdAsync(id);
        if (staff == null) throw new KeyNotFoundException("Staff not found");
        if (positionId != null) staff.UpdatePosition(positionId);
        if (airlineId != null) staff.UpdateAirline(airlineId);
        if (airportId != null) staff.UpdateAirport(airportId);
        if (hireDate != null) staff.UpdateHireDate(hireDate);
        if (isActive != null) staff.UpdateIsActive(isActive);
        await _repository.UpdateAsync(staff);
    }
}