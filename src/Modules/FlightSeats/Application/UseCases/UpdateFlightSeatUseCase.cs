using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Application.UseCases;

public sealed class UpdateFlightSeatUseCase
{
    private readonly IFlightSeatRepository _repository;

    public UpdateFlightSeatUseCase(IFlightSeatRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<FlightSeat?> ExecuteAsync(
        int id,
        int? flightId = null,
        string? seatCode = null,
        int? cabinTypeId = null,
        int? locationTypeId = null,
        bool? isOccupied = null,
        CancellationToken cancellationToken = default)
    {
        var flightSeat = await _repository.GetByIdAsync(new FlightSeatId(id), cancellationToken);
        
        if (flightSeat == null)
            return null;

        if (flightId.HasValue)
            flightSeat.UpdateFlightId(new FlightId(flightId.Value));

        if (!string.IsNullOrEmpty(seatCode))
            flightSeat.UpdateSeatCode(new SeatCode(seatCode));

        if (cabinTypeId.HasValue)
            flightSeat.UpdateCabinTypeId(new CabinTypeId(cabinTypeId.Value));

        if (locationTypeId.HasValue)
            flightSeat.UpdateLocationTypeId(new LocationTypeId(locationTypeId.Value));

        if (isOccupied.HasValue)
        {
            if (isOccupied.Value)
                flightSeat.MarkAsOccupied();
            else
                flightSeat.MarkAsAvailable();
        }

        return await _repository.UpdateAsync(flightSeat, cancellationToken);
    }
}
