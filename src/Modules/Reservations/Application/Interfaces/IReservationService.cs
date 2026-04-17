using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Interfaces;

public interface IReservationService
{
    Task<Reservation> CreateAsync(
        CreateReservationRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Reservation?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Reservation>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateReservationRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
