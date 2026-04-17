using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.UseCases;

public interface IDeleteSystemRoleUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteSystemRoleUseCase : IDeleteSystemRoleUseCase
{
    private readonly ISystemRoleRepository _repository;

    public DeleteSystemRoleUseCase(ISystemRoleRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(SystemRoleId.Create(id), cancellationToken);
    }
}
