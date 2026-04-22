using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.UseCases;

public interface ICreateFlightUseCase
{
    Task<Flight> ExecuteAsync(
        CreateFlightRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateFlightUseCase : ICreateFlightUseCase
{
    private readonly IFlightRepository _repository;

    public CreateFlightUseCase(IFlightRepository repository)
    {
        _repository = repository;
    }

    public Task<Flight> ExecuteAsync(
        CreateFlightRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var flightCode = request.FlightCode;
        ArgumentNullException.ThrowIfNull(flightCode);
        var x = Flight.Create(new FlightId(0), FlightCode.Create(flightCode), FlightAirlineId.Create(request.AirlineId), FlightRouteId.Create(request.RouteId), FlightAircraftId.Create(request.AircraftId), FlightDepartureDate.Create(request.DepartureDate), FlightEstimatedArrivalDate.Create(request.EstimatedArrivalDate), FlightTotalCapacity.Create(request.TotalCapacity), FlightAvailableSeats.Create(request.AvailableSeats), FlightStatusId.Create(request.FlightStatusId), FlightRescheduledAt.Create(request.RescheduledAt), FlightCreatedAt.Create(request.CreatedAt), FlightUpdatedAt.Create(request.UpdatedAt));
        return _repository.AddAsync(x, cancellationToken);
    }
}
