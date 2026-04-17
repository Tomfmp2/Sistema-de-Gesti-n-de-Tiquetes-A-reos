using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.UseCases;

public sealed class DeleteFlightAssignmentUseCase
{
    private readonly IFlightAssignmentRepository _repository;

    public DeleteFlightAssignmentUseCase(IFlightAssignmentRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> ExecuteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(new FlightAssignmentId(id), cancellationToken);
    }
}
