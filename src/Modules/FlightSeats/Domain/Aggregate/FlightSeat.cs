using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.Aggregate;

public sealed class FlightSeat
{
    public FlightSeatId Id { get; private set; }
    public FlightId FlightId { get; private set; }
    public SeatCode SeatCode { get; private set; }
    public CabinTypeId CabinTypeId { get; private set; }
    public LocationTypeId LocationTypeId { get; private set; }
    public bool IsOccupied { get; private set; }

    private FlightSeat(
        FlightSeatId id,
        FlightId flightId,
        SeatCode seatCode,
        CabinTypeId cabinTypeId,
        LocationTypeId locationTypeId,
        bool isOccupied)
    {
        Id = id;
        FlightId = flightId;
        SeatCode = seatCode;
        CabinTypeId = cabinTypeId;
        LocationTypeId = locationTypeId;
        IsOccupied = isOccupied;
    }

    public static FlightSeat Create(
        FlightId flightId,
        SeatCode seatCode,
        CabinTypeId cabinTypeId,
        LocationTypeId locationTypeId,
        bool isOccupied = false)
    {
        return new FlightSeat(
            new FlightSeatId(0),
            flightId,
            seatCode,
            cabinTypeId,
            locationTypeId,
            isOccupied);
    }

    public static FlightSeat Reconstitute(
        FlightSeatId id,
        FlightId flightId,
        SeatCode seatCode,
        CabinTypeId cabinTypeId,
        LocationTypeId locationTypeId,
        bool isOccupied)
    {
        return new FlightSeat(id, flightId, seatCode, cabinTypeId, locationTypeId, isOccupied);
    }

    public void UpdateFlightId(FlightId flightId)
    {
        FlightId = flightId ?? throw new ArgumentNullException(nameof(flightId));
    }

    public void UpdateSeatCode(SeatCode seatCode)
    {
        SeatCode = seatCode ?? throw new ArgumentNullException(nameof(seatCode));
    }

    public void UpdateCabinTypeId(CabinTypeId cabinTypeId)
    {
        CabinTypeId = cabinTypeId ?? throw new ArgumentNullException(nameof(cabinTypeId));
    }

    public void UpdateLocationTypeId(LocationTypeId locationTypeId)
    {
        LocationTypeId = locationTypeId ?? throw new ArgumentNullException(nameof(locationTypeId));
    }

    public void MarkAsOccupied()
    {
        IsOccupied = true;
    }

    public void MarkAsAvailable()
    {
        IsOccupied = false;
    }
}
