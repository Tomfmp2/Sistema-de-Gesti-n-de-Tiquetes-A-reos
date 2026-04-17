using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Application.UseCases;

public interface IUpdateReservationFlightUseCase
{
    Task ExecuteAsync(
        UpdateReservationFlightRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateReservationFlightUseCase : IUpdateReservationFlightUseCase
{
    private readonly IReservationFlightRepository _repository;

    public UpdateReservationFlightUseCase(IReservationFlightRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateReservationFlightRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = ReservationFlight.Create(ReservationFlightId.Create(request.Id), ReservationFlightReservationId.Create(request.ReservationId), ReservationFlightFlightId.Create(request.FlightId), ReservationFlightPartialValue.Create(request.PartialValue));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
