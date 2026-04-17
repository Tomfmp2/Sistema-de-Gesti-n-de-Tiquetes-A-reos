using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.UseCases;

public interface ICreateSystemRoleUseCase
{
    Task<SystemRole> ExecuteAsync(
        CreateSystemRoleRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateSystemRoleUseCase : ICreateSystemRoleUseCase
{
    private readonly ISystemRoleRepository _repository;

    public CreateSystemRoleUseCase(ISystemRoleRepository repository)
    {
        _repository = repository;
    }

    public Task<SystemRole> ExecuteAsync(
        CreateSystemRoleRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = SystemRole.CreateNew(SystemRoleName.Create(request.Name), request.Description);
        return _repository.AddAsync(x, cancellationToken);
    }
}
