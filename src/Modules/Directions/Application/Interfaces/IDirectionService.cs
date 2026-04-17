using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Application.Interfaces;

public interface IDirectionService
{
    Task<Direction> CreateAsync(
        CreateDirectionRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Direction?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Direction>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateDirectionRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
