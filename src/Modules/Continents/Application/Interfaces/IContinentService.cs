using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.Interfaces;

public interface IContinentService
{
    Task<Continent> CreateAsync(
        CreateContinentRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Continent?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Continent>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateContinentRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
