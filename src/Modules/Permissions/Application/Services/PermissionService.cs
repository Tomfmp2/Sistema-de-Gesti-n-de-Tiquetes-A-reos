using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.Services;

public sealed class PermissionService : IPermissionService
{
    private readonly ICreatePermissionUseCase _create;
    private readonly IGetPermissionByIdUseCase _getById;
    private readonly IGetAllPermissionsUseCase _getAll;
    private readonly IUpdatePermissionUseCase _update;
    private readonly IDeletePermissionUseCase _delete;

    public PermissionService(
        ICreatePermissionUseCase create,
        IGetPermissionByIdUseCase getById,
        IGetAllPermissionsUseCase getAll,
        IUpdatePermissionUseCase update,
        IDeletePermissionUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Permission> CreateAsync(
        CreatePermissionRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Permission?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdatePermissionRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}
