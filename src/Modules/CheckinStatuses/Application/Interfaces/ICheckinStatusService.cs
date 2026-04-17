using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.Interfaces;

public interface ICheckinStatusService
{
    Task<CheckinStatus> CreateAsync(
        CreateCheckinStatusRequest request,
        CancellationToken cancellationToken = default
    );

    Task<CheckinStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CheckinStatus>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateCheckinStatusRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
