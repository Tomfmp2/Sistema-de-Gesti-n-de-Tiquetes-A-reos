using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.UseCases;

public sealed class GetFlightAssignmentByIdUseCase
{
    private readonly IFlightAssignmentRepository _repository;

    public GetFlightAssignmentByIdUseCase(IFlightAssignmentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<FlightAssignment?> ExecuteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(new FlightAssignmentId(id), cancellationToken);
    }
}
