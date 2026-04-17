using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.Interfaces;

public interface ICityService
{
    Task<City> CreateAsync(
        CreateCityRequest request,
        CancellationToken cancellationToken = default
    );

    Task<City?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<City>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(UpdateCityRequest request, CancellationToken cancellationToken = default);

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
