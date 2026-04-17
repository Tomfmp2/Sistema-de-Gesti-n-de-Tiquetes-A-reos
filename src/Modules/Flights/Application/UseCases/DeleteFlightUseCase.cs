using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.UseCases;

public interface IDeleteFlightUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteFlightUseCase : IDeleteFlightUseCase
{
    private readonly IFlightRepository _repository;

    public DeleteFlightUseCase(IFlightRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(FlightId.Create(id), cancellationToken);
    }
}
