using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Repositories;

public interface IReservationStatusRepository
{
    Task<ReservationStatus?> GetByIdAsync(ReservationStatusId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservationStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ReservationStatus> AddAsync(ReservationStatus entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(ReservationStatus entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(ReservationStatusId id, CancellationToken cancellationToken = default);
}
