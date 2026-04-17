using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.Interfaces;

public interface ICountryService
{
    Task<Country> CreateAsync(
        CreateCountryRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Country?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateCountryRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
