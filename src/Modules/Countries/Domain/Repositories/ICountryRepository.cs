using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Repositories;

public interface ICountryRepository
{
    Task<Country?> GetByIdAsync(CountryId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Country> AddAsync(Country entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Country entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(CountryId id, CancellationToken cancellationToken = default);
}
