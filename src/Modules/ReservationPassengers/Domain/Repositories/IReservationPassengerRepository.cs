using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Repositories;

public interface IReservationPassengerRepository
{
    Task<ReservationPassenger?> GetByIdAsync(ReservationPassengerId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservationPassenger>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ReservationPassenger> AddAsync(ReservationPassenger entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(ReservationPassenger entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(ReservationPassengerId id, CancellationToken cancellationToken = default);
}
