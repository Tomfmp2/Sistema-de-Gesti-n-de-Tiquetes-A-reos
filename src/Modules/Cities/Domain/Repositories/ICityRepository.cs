using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Repositories;

public interface ICityRepository
{
    Task<City?> GetByIdAsync(CityId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<City>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<City> AddAsync(City entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(City entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(CityId id, CancellationToken cancellationToken = default);
}
