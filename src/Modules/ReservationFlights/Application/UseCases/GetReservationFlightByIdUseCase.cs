using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.UseCases;

public interface IGetReservationFlightByIdUseCase
{
    Task<ReservationFlight?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetReservationFlightByIdUseCase : IGetReservationFlightByIdUseCase
{
    private readonly IReservationFlightRepository _repository;

    public GetReservationFlightByIdUseCase(IReservationFlightRepository repository)
    {
        _repository = repository;
    }

    public Task<ReservationFlight?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<ReservationFlight?>(null);
        }

        return _repository.GetByIdAsync(ReservationFlightId.Create(id), cancellationToken);
    }
}
