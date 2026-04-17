using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.UseCases;

public interface IGetAllSystemRolesUseCase
{
    Task<IReadOnlyList<SystemRole>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllSystemRolesUseCase : IGetAllSystemRolesUseCase
{
    private readonly ISystemRoleRepository _repository;

    public GetAllSystemRolesUseCase(ISystemRoleRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<SystemRole>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
