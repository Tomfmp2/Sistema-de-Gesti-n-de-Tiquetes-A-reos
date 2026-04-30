using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Application.UseCases;

public sealed class CreateFlightSeatUseCase
{
    private readonly IFlightSeatRepository _repository;

    public CreateFlightSeatUseCase(IFlightSeatRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<FlightSeat> ExecuteAsync(
        int flightId,
        string seatCode,
        int cabinTypeId,
        int locationTypeId,
        bool isOccupied = false,
        CancellationToken cancellationToken = default)
    {
        var flightSeat = FlightSeat.Create(
            new FlightId(flightId),
            new SeatCode(seatCode),
            new CabinTypeId(cabinTypeId),
            new LocationTypeId(locationTypeId),
            isOccupied);

        return await _repository.AddAsync(flightSeat, cancellationToken);
    }
}
