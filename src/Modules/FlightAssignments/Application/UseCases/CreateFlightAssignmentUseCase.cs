using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.UseCases;

public sealed class CreateFlightAssignmentUseCase
{
    private readonly IFlightAssignmentRepository _repository;

    public CreateFlightAssignmentUseCase(IFlightAssignmentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<FlightAssignment> ExecuteAsync(
        int flightId,
        int staffId,
        int flightRoleId,
        CancellationToken cancellationToken = default)
    {
        var flightAssignment = FlightAssignment.Create(
            new FlightId(flightId),
            new StaffId(staffId),
            new FlightRoleId(flightRoleId));

        return await _repository.AddAsync(flightAssignment, cancellationToken);
    }
}
