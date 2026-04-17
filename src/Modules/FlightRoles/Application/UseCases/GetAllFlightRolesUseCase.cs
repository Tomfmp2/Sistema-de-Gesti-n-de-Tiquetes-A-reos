using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.UseCases;

public interface IGetAllFlightRolesUseCase
{
    Task<IReadOnlyList<FlightRole>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllFlightRolesUseCase : IGetAllFlightRolesUseCase
{
    private readonly IFlightRoleRepository _repository;

    public GetAllFlightRolesUseCase(IFlightRoleRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<FlightRole>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(cancellationToken);
    }
}
