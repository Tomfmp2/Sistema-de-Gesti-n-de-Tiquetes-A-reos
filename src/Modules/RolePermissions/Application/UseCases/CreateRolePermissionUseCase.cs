using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Application.UseCases;

public interface ICreateRolePermissionUseCase
{
    Task<RolePermission> ExecuteAsync(
        CreateRolePermissionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateRolePermissionUseCase : ICreateRolePermissionUseCase
{
    private readonly IRolePermissionRepository _repository;

    public CreateRolePermissionUseCase(IRolePermissionRepository repository)
    {
        _repository = repository;
    }

    public Task<RolePermission> ExecuteAsync(
        CreateRolePermissionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = RolePermission.CreateNew(
            RolePermissionRoleId.Create(request.RoleId),
            RolePermissionPermissionId.Create(request.PermissionId)
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}
