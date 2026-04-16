using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.Interfaces;

public interface ISystemRoleService
{
    Task<SystemRole> CreateAsync(
        CreateSystemRoleRequest request,
        CancellationToken cancellationToken = default
    );

    Task<SystemRole?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SystemRole>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateSystemRoleRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}
