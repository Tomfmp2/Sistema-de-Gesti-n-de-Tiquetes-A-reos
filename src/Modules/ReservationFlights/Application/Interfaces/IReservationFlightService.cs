using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.Interfaces;

public interface IReservationFlightService
{
    Task<ReservationFlight> CreateAsync(
        CreateReservationFlightRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ReservationFlight?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ReservationFlight>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateReservationFlightRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
