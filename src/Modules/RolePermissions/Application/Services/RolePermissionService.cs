using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.Services;

public sealed class RolePermissionService : IRolePermissionService
{
    private readonly ICreateRolePermissionUseCase _create;
    private readonly IGetRolePermissionByIdUseCase _getById;
    private readonly IGetAllRolePermissionsUseCase _getAll;
    private readonly IUpdateRolePermissionUseCase _update;
    private readonly IDeleteRolePermissionUseCase _delete;

    public RolePermissionService(
        ICreateRolePermissionUseCase create,
        IGetRolePermissionByIdUseCase getById,
        IGetAllRolePermissionsUseCase getAll,
        IUpdateRolePermissionUseCase update,
        IDeleteRolePermissionUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<RolePermission> CreateAsync(
        CreateRolePermissionRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<RolePermission?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<RolePermission>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateRolePermissionRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
