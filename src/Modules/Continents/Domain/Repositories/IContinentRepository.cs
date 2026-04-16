using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Repositories;

public interface IContinentRepository
{
    Task<Continent?> GetByIdAsync(ContinentId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Continent>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Continent> AddAsync(Continent entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Continent entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(ContinentId id, CancellationToken cancellationToken = default);
}
