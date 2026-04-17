using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Repositories;

public interface IDirectionRepository
{
    Task<Direction?> GetByIdAsync(DirectionId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Direction>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Direction> AddAsync(Direction entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Direction entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(DirectionId id, CancellationToken cancellationToken = default);
}
