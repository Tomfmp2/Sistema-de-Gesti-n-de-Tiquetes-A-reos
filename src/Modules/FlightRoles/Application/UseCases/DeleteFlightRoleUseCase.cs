using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.UseCases;

public interface IDeleteFlightRoleUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteFlightRoleUseCase : IDeleteFlightRoleUseCase
{
    private readonly IFlightRoleRepository _repository;

    public DeleteFlightRoleUseCase(IFlightRoleRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(FlightRoleId.Create(id), cancellationToken);
    }
}
