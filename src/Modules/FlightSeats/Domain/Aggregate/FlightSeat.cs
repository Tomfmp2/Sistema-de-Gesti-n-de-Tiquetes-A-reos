using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.Aggregate;

public sealed class FlightSeat
{
    public FlightSeatId Id { get; private set; }
    public FlightId FlightId { get; private set; }
    public SeatCode SeatCode { get; private set; }
    public CabinTypeId CabinTypeId { get; private set; }
    public LocationTypeId LocationTypeId { get; private set; }
    public SeatStatus Status { get; private set; }

    private FlightSeat(
        FlightSeatId id,
        FlightId flightId,
        SeatCode seatCode,
        CabinTypeId cabinTypeId,
        LocationTypeId locationTypeId,
        SeatStatus status)
    {
        Id = id;
        FlightId = flightId;
        SeatCode = seatCode;
        CabinTypeId = cabinTypeId;
        LocationTypeId = locationTypeId;
        Status = status;
    }

    public static FlightSeat Create(
        FlightId flightId,
        SeatCode seatCode,
        CabinTypeId cabinTypeId,
        LocationTypeId locationTypeId,
        SeatStatus? status = null)
    {
        return new FlightSeat(
            new FlightSeatId(0),
            flightId,
            seatCode,
            cabinTypeId,
            locationTypeId,
            status ?? SeatStatus.Create("Disponible"));
    }

    public static FlightSeat Reconstitute(
        FlightSeatId id,
        FlightId flightId,
        SeatCode seatCode,
        CabinTypeId cabinTypeId,
        LocationTypeId locationTypeId,
        SeatStatus status)
    {
        return new FlightSeat(id, flightId, seatCode, cabinTypeId, locationTypeId, status);
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
        Status = SeatStatus.Create("Ocupado");
    }

    public void MarkAsAvailable()
    {
        Status = SeatStatus.Create("Disponible");
    }

    public void MarkAsReserved()
    {
        Status = SeatStatus.Create("Reservado");
    }
}
