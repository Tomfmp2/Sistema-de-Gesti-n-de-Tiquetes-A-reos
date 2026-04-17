using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.UseCases;

public interface IGetAllRolePermissionsUseCase
{
    Task<IReadOnlyList<RolePermission>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllRolePermissionsUseCase : IGetAllRolePermissionsUseCase
{
    private readonly IRolePermissionRepository _repository;

    public GetAllRolePermissionsUseCase(IRolePermissionRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<RolePermission>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
