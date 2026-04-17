using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.UseCases;

public interface IDeleteReservationFlightUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteReservationFlightUseCase : IDeleteReservationFlightUseCase
{
    private readonly IReservationFlightRepository _repository;

    public DeleteReservationFlightUseCase(IReservationFlightRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(ReservationFlightId.Create(id), cancellationToken);
    }
}
