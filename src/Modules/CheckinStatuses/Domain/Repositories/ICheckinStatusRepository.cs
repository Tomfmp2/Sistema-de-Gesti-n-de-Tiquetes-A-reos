using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Repositories;

public interface ICheckinStatusRepository
{
    Task<CheckinStatus?> GetByIdAsync(CheckinStatusId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CheckinStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CheckinStatus> AddAsync(CheckinStatus entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(CheckinStatus entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(CheckinStatusId id, CancellationToken cancellationToken = default);
}
