using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.UseCases;

public interface IGetAllPermissionsUseCase
{
    Task<IReadOnlyList<Permission>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllPermissionsUseCase : IGetAllPermissionsUseCase
{
    private readonly IPermissionRepository _repository;

    public GetAllPermissionsUseCase(IPermissionRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Permission>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
