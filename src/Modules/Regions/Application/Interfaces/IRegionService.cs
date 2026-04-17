using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.Interfaces;

public interface IRegionService
{
    Task<Region> CreateAsync(
        CreateRegionRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Region?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Region>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateRegionRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
