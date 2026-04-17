using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.UseCases;

public interface ICreateFlightRoleUseCase
{
    Task<FlightRole> ExecuteAsync(CreateFlightRoleRequest request, CancellationToken cancellationToken = default);
}

public sealed class CreateFlightRoleUseCase : ICreateFlightRoleUseCase
{
    private readonly IFlightRoleRepository _repository;

    public CreateFlightRoleUseCase(IFlightRoleRepository repository)
    {
        _repository = repository;
    }

    public Task<FlightRole> ExecuteAsync(CreateFlightRoleRequest request, CancellationToken cancellationToken = default)
    {
        var entity = FlightRole.Create(FlightRoleName.Create(request.Name));
        return _repository.AddAsync(entity, cancellationToken);
    }
}
