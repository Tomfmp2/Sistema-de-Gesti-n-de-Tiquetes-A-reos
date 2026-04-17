using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.UseCases;

public interface IGetSystemRoleByIdUseCase
{
    Task<SystemRole?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetSystemRoleByIdUseCase : IGetSystemRoleByIdUseCase
{
    private readonly ISystemRoleRepository _repository;

    public GetSystemRoleByIdUseCase(ISystemRoleRepository repository)
    {
        _repository = repository;
    }

    public Task<SystemRole?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<SystemRole?>(null);
        }

        return _repository.GetByIdAsync(SystemRoleId.Create(id), cancellationToken);
    }
}
