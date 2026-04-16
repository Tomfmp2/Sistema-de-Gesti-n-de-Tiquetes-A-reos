using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.Services;

public sealed class SystemRoleService : ISystemRoleService
{
    private readonly ICreateSystemRoleUseCase _create;
    private readonly IGetSystemRoleByIdUseCase _getById;
    private readonly IGetAllSystemRolesUseCase _getAll;
    private readonly IUpdateSystemRoleUseCase _update;
    private readonly IDeleteSystemRoleUseCase _delete;

    public SystemRoleService(
        ICreateSystemRoleUseCase create,
        IGetSystemRoleByIdUseCase getById,
        IGetAllSystemRolesUseCase getAll,
        IUpdateSystemRoleUseCase update,
        IDeleteSystemRoleUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<SystemRole> CreateAsync(
        CreateSystemRoleRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<SystemRole?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<SystemRole>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateSystemRoleRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
