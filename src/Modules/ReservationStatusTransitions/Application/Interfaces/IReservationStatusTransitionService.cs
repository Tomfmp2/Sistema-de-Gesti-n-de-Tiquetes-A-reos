using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.Interfaces;

public interface IReservationStatusTransitionService
{
    Task<ReservationStatusTransition> CreateAsync(
        CreateReservationStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ReservationStatusTransition?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ReservationStatusTransition>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateReservationStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
