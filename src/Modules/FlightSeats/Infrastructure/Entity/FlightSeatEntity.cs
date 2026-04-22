using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;

public sealed class FlightSeatEntity
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public string SeatCode { get; set; } = string.Empty;
    public int CabinTypeId { get; set; }
    public int LocationTypeId { get; set; }
    public bool IsOccupied { get; set; }

    public FlightEntity? Flight { get; set; }
    public CabinTypeEntity? CabinType { get; set; }
    public SeatLocationTypeEntity? LocationType { get; set; }
    public CheckinEntity? Checkin { get; set; }
}