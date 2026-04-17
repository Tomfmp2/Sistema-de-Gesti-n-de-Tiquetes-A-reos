using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Repositories;

public interface IStreetTypeRepository
{
    Task<StreetType?> GetByIdAsync(StreetTypeId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<StreetType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<StreetType> AddAsync(StreetType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(StreetType entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(StreetTypeId id, CancellationToken cancellationToken = default);
}
