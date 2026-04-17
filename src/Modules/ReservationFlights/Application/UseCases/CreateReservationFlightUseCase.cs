using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.UseCases;

public interface ICreateReservationFlightUseCase
{
    Task<ReservationFlight> ExecuteAsync(
        CreateReservationFlightRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateReservationFlightUseCase : ICreateReservationFlightUseCase
{
    private readonly IReservationFlightRepository _repository;

    public CreateReservationFlightUseCase(IReservationFlightRepository repository)
    {
        _repository = repository;
    }

    public Task<ReservationFlight> ExecuteAsync(
        CreateReservationFlightRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = ReservationFlight.Create(new ReservationFlightId(0), ReservationFlightReservationId.Create(request.ReservationId), ReservationFlightFlightId.Create(request.FlightId), ReservationFlightPartialValue.Create(request.PartialValue));
        return _repository.AddAsync(x, cancellationToken);
    }
}
