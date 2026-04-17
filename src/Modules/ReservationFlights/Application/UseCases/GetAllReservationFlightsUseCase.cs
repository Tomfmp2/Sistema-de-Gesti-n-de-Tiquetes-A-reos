using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.UseCases;

public interface IGetAllReservationFlightsUseCase
{
    Task<IReadOnlyList<ReservationFlight>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllReservationFlightsUseCase : IGetAllReservationFlightsUseCase
{
    private readonly IReservationFlightRepository _repository;

    public GetAllReservationFlightsUseCase(IReservationFlightRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<ReservationFlight>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}
