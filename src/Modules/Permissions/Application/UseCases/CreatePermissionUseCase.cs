using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.UseCases;

public interface ICreatePermissionUseCase
{
    Task<Permission> ExecuteAsync(
        CreatePermissionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreatePermissionUseCase : ICreatePermissionUseCase
{
    private readonly IPermissionRepository _repository;

    public CreatePermissionUseCase(IPermissionRepository repository)
    {
        _repository = repository;
    }

    public Task<Permission> ExecuteAsync(
        CreatePermissionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Permission.CreateNew(PermissionName.Create(request.Name), request.Description);
        return _repository.AddAsync(x, cancellationToken);
    }
}
