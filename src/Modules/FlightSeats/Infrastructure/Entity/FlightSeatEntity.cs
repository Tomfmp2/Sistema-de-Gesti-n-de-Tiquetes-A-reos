namespace sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Infrastructure.Entity;

public sealed class FlightSeatEntity
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public string SeatCode { get; set; } = string.Empty;
    public int CabinTypeId { get; set; }
    public int LocationTypeId { get; set; }
    public bool IsOccupied { get; set; }
}
