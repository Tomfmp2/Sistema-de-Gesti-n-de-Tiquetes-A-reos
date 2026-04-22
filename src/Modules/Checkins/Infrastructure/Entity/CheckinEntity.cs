using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Infrastructure.Entity;

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

    public TicketEntity? Ticket { get; set; }
    public StaffEntity? Staff { get; set; }
    public FlightSeatEntity? FlightSeat { get; set; }
    public CheckinStatusEntity? CheckinStatus { get; set; }
    public ICollection<BaggageEntity> Baggages { get; set; } = new List<BaggageEntity>();
}
