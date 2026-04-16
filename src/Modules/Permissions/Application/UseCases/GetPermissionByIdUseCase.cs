using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.UseCases;

public interface IGetPermissionByIdUseCase
{
    Task<Permission?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetPermissionByIdUseCase : IGetPermissionByIdUseCase
{
    private readonly IPermissionRepository _repository;

    public GetPermissionByIdUseCase(IPermissionRepository repository)
    {
        _repository = repository;
    }

    public Task<Permission?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Permission?>(null);
        }

        return _repository.GetByIdAsync(PermissionId.Create(id), cancellationToken);
    }
}
