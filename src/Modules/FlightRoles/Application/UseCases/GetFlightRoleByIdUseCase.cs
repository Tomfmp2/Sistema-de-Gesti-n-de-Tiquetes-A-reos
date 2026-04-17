using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.UseCases;

public interface IGetFlightRoleByIdUseCase
{
    Task<FlightRole?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetFlightRoleByIdUseCase : IGetFlightRoleByIdUseCase
{
    private readonly IFlightRoleRepository _repository;

    public GetFlightRoleByIdUseCase(IFlightRoleRepository repository)
    {
        _repository = repository;
    }

    public Task<FlightRole?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<FlightRole?>(null);
        }

        return _repository.GetByIdAsync(FlightRoleId.Create(id), cancellationToken);
    }
}
