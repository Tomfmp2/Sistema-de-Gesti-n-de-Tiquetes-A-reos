using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Repositories;

public interface IRegionRepository
{
    Task<Region?> GetByIdAsync(RegionId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Region>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Region> AddAsync(Region entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Region entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(RegionId id, CancellationToken cancellationToken = default);
}
