using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Repositories;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(PermissionId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Permission> AddAsync(Permission entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Permission entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(PermissionId id, CancellationToken cancellationToken = default);
}
