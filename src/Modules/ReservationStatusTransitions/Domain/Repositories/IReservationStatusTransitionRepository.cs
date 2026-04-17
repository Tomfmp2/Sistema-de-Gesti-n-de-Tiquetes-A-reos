using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Repositories;

public interface IReservationStatusTransitionRepository
{
    Task<ReservationStatusTransition?> GetByIdAsync(ReservationStatusTransitionId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservationStatusTransition>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ReservationStatusTransition> AddAsync(ReservationStatusTransition entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(ReservationStatusTransition entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(ReservationStatusTransitionId id, CancellationToken cancellationToken = default);
}
