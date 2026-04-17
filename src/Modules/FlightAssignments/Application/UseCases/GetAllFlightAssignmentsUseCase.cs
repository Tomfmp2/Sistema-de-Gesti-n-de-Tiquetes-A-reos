using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightAssignments.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightAssignments.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.src.Modules.FlightAssignments.Application.UseCases;

public sealed class GetAllFlightAssignmentsUseCase
{
    private readonly IFlightAssignmentRepository _repository;

    public GetAllFlightAssignmentsUseCase(IFlightAssignmentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<FlightAssignment>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}
