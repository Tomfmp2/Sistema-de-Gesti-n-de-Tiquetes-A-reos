using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightAssignments.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightAssignments.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.src.Modules.FlightAssignments.Application.Interfaces;

public interface IFlightAssignmentRepository
{
    Task<FlightAssignment?> GetByIdAsync(FlightAssignmentId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<FlightAssignment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FlightAssignment> AddAsync(FlightAssignment flightAssignment, CancellationToken cancellationToken = default);
    Task<FlightAssignment> UpdateAsync(FlightAssignment flightAssignment, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(FlightAssignmentId id, CancellationToken cancellationToken = default);
}
