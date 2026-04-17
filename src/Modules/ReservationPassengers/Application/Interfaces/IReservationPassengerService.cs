using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.Interfaces;

public interface IReservationPassengerService
{
    Task<ReservationPassenger> CreateAsync(
        CreateReservationPassengerRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ReservationPassenger?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ReservationPassenger>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateReservationPassengerRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
