using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.UseCases;

public interface IUpdateRolePermissionUseCase
{
    Task ExecuteAsync(
        UpdateRolePermissionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateRolePermissionUseCase : IUpdateRolePermissionUseCase
{
    private readonly IRolePermissionRepository _repository;

    public UpdateRolePermissionUseCase(IRolePermissionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateRolePermissionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = RolePermission.Create(
            RolePermissionId.Create(request.Id),
            RolePermissionRoleId.Create(request.RoleId),
            RolePermissionPermissionId.Create(request.PermissionId)
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
