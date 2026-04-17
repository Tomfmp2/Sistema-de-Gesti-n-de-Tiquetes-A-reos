using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Application.UseCases;

public interface IUpdateSystemRoleUseCase
{
    Task ExecuteAsync(
        UpdateSystemRoleRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateSystemRoleUseCase : IUpdateSystemRoleUseCase
{
    private readonly ISystemRoleRepository _repository;

    public UpdateSystemRoleUseCase(ISystemRoleRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateSystemRoleRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = SystemRole.Create(
            SystemRoleId.Create(request.Id),
            SystemRoleName.Create(request.Name),
            request.Description
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
