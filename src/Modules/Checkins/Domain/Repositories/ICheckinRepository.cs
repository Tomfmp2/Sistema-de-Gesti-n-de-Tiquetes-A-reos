using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Repositories;

public interface ICheckinRepository
{
    Task<Checkin?> GetByIdAsync(CheckinId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Checkin>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Checkin> AddAsync(Checkin entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Checkin entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(CheckinId id, CancellationToken cancellationToken = default);
}
