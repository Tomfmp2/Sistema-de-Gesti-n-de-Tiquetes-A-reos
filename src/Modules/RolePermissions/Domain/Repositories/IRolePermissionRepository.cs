using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Repositories;

public interface IRolePermissionRepository
{
    Task<RolePermission?> GetByIdAsync(
        RolePermissionId id,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<RolePermission>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<RolePermission> AddAsync(RolePermission entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(RolePermission entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(RolePermissionId id, CancellationToken cancellationToken = default);
}
