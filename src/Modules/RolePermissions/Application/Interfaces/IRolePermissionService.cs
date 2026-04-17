using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.Interfaces;

public interface IRolePermissionService
{
    Task<RolePermission> CreateAsync(
        CreateRolePermissionRequest request,
        CancellationToken cancellationToken = default
    );

    Task<RolePermission?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<RolePermission>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateRolePermissionRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
