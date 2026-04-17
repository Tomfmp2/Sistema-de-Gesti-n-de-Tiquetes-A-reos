using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.UseCases;

public class CreateStaffUseCase
{
    private readonly IStaffRepository _repository;

    public CreateStaffUseCase(IStaffRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        PersonId personId,
        PositionId positionId,
        AirlineId? airlineId,
        AirportId? airportId,
        HireDate hireDate,
        IsActive isActive)
    {
        // Generate new ID (in real app, use a proper ID generator)
        var id = StaffId.Create(new Random().Next(1, int.MaxValue));
        var staff = StaffRecord.Create(id, personId, positionId, airlineId, airportId, hireDate, isActive);
        await _repository.AddAsync(staff);
    }
}