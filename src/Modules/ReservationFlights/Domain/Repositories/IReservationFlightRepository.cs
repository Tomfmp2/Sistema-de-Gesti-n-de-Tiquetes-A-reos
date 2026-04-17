using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Repositories;

public interface IReservationFlightRepository
{
    Task<ReservationFlight?> GetByIdAsync(ReservationFlightId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservationFlight>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ReservationFlight> AddAsync(ReservationFlight entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(ReservationFlight entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(ReservationFlightId id, CancellationToken cancellationToken = default);
}
