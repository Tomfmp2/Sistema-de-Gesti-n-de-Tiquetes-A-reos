namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Infrastructure.Entity;

public class CheckinEntity
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public int StaffId { get; set; }
    public int FlightSeatId { get; set; }
    public DateTime CheckinDate { get; set; }
    public int CheckinStatusId { get; set; }
    public string? BoardingPassNumber { get; set; }
    public bool HasCheckedBaggage { get; set; }
    public decimal? BaggageWeightKg { get; set; }
}
