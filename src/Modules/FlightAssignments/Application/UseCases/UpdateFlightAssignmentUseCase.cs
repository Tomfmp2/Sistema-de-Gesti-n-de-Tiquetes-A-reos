using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.UseCases;

public sealed class UpdateFlightAssignmentUseCase
{
    private readonly IFlightAssignmentRepository _repository;

    public UpdateFlightAssignmentUseCase(IFlightAssignmentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<FlightAssignment?> ExecuteAsync(
        int id,
        int? flightId = null,
        int? staffId = null,
        int? flightRoleId = null,
        CancellationToken cancellationToken = default)
    {
        var flightAssignment = await _repository.GetByIdAsync(new FlightAssignmentId(id), cancellationToken);
        
        if (flightAssignment == null)
            return null;

        if (flightId.HasValue)
            flightAssignment.UpdateFlightId(new FlightId(flightId.Value));

        if (staffId.HasValue)
            flightAssignment.UpdateStaffId(new StaffId(staffId.Value));

        if (flightRoleId.HasValue)
            flightAssignment.UpdateFlightRoleId(new FlightRoleId(flightRoleId.Value));

        return await _repository.UpdateAsync(flightAssignment, cancellationToken);
    }
}
