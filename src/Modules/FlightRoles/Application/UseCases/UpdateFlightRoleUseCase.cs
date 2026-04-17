using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.UseCases;

public interface IUpdateFlightRoleUseCase
{
    Task ExecuteAsync(UpdateFlightRoleRequest request, CancellationToken cancellationToken = default);
}

public sealed class UpdateFlightRoleUseCase : IUpdateFlightRoleUseCase
{
    private readonly IFlightRoleRepository _repository;

    public UpdateFlightRoleUseCase(IFlightRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(UpdateFlightRoleRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(FlightRoleId.Create(request.Id), cancellationToken);

        if (entity is null)
        {
            throw new InvalidOperationException($"No existe flight role {request.Id}.");
        }

        entity = entity.WithName(FlightRoleName.Create(request.Name));
        await _repository.UpdateAsync(entity, cancellationToken);
    }
}
