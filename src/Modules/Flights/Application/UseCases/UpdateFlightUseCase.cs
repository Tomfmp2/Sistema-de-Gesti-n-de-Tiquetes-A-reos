using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.UseCases;

public interface IUpdateFlightUseCase
{
    Task ExecuteAsync(
        UpdateFlightRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateFlightUseCase : IUpdateFlightUseCase
{
    private readonly IFlightRepository _repository;

    public UpdateFlightUseCase(IFlightRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateFlightRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Flight.Create(FlightId.Create(request.Id), FlightCode.Create(request.FlightCode), FlightAirlineId.Create(request.AirlineId), FlightRouteId.Create(request.RouteId), FlightAircraftId.Create(request.AircraftId), FlightDepartureDate.Create(request.DepartureDate), FlightEstimatedArrivalDate.Create(request.EstimatedArrivalDate), FlightTotalCapacity.Create(request.TotalCapacity), FlightAvailableSeats.Create(request.AvailableSeats), FlightStatusId.Create(request.FlightStatusId), FlightRescheduledAt.Create(request.RescheduledAt), FlightCreatedAt.Create(request.CreatedAt), FlightUpdatedAt.Create(request.UpdatedAt));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}
