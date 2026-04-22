using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Repositories;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync(ReservationId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Reservation>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Reservation>> GetAllByClientIdAsync(int clientId, CancellationToken cancellationToken = default);
    Task<Reservation> AddAsync(Reservation entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Reservation entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(ReservationId id, CancellationToken cancellationToken = default);
}
