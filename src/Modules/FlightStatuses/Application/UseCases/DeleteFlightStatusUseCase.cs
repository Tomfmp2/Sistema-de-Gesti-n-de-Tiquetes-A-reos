using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;

public interface IDeleteFlightStatusUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteFlightStatusUseCase : IDeleteFlightStatusUseCase
{
    private readonly IFlightStatusRepository _repository;

    public DeleteFlightStatusUseCase(IFlightStatusRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(FlightStatusId.Create(id), cancellationToken);
    }
}
