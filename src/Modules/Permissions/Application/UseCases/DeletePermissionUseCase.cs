using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Application.UseCases;

public interface IDeletePermissionUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeletePermissionUseCase : IDeletePermissionUseCase
{
    private readonly IPermissionRepository _repository;

    public DeletePermissionUseCase(IPermissionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(PermissionId.Create(id), cancellationToken);
    }
}
