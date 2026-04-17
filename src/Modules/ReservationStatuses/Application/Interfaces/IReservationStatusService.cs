using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.Interfaces;

public interface IReservationStatusService
{
    Task<ReservationStatus> CreateAsync(
        CreateReservationStatusRequest request,
        CancellationToken cancellationToken = default
    );

    Task<ReservationStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ReservationStatus>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateReservationStatusRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
