using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.Interfaces;

public interface IPermissionService
{
    Task<Permission> CreateAsync(
        CreatePermissionRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Permission?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdatePermissionRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
