using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Repositories;

public interface ISystemRoleRepository
{
    Task<SystemRole?> GetByIdAsync(SystemRoleId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<SystemRole>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<SystemRole> AddAsync(SystemRole entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(SystemRole entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(SystemRoleId id, CancellationToken cancellationToken = default);
}
