using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.UseCases;

public interface IUpdatePermissionUseCase
{
    Task ExecuteAsync(
        UpdatePermissionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdatePermissionUseCase : IUpdatePermissionUseCase
{
    private readonly IPermissionRepository _repository;

    public UpdatePermissionUseCase(IPermissionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdatePermissionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Permission.Create(
            PermissionId.Create(request.Id),
            PermissionName.Create(request.Name),
            request.Description
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
