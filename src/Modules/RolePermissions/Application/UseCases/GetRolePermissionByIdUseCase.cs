using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.UseCases;

public interface IGetRolePermissionByIdUseCase
{
    Task<RolePermission?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetRolePermissionByIdUseCase : IGetRolePermissionByIdUseCase
{
    private readonly IRolePermissionRepository _repository;

    public GetRolePermissionByIdUseCase(IRolePermissionRepository repository)
    {
        _repository = repository;
    }

    public Task<RolePermission?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<RolePermission?>(null);
        }

        return _repository.GetByIdAsync(RolePermissionId.Create(id), cancellationToken);
    }
}
