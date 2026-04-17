using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Repositories;

public interface IFlightStatusTransitionRepository
{
    Task<FlightStatusTransition?> GetByIdAsync(FlightStatusTransitionId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FlightStatusTransition>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FlightStatusTransition> AddAsync(FlightStatusTransition entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(FlightStatusTransition entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(FlightStatusTransitionId id, CancellationToken cancellationToken = default);
}
