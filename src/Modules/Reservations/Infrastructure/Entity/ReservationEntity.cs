using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;

public class ReservationEntity
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public DateTime ReservationDate { get; set; }
    public int ReservationStatusId { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public ClientEntity? Client { get; set; }
    public ReservationStatusEntity? ReservationStatus { get; set; }
    public ICollection<ReservationFlightEntity> ReservationFlights { get; set; } = new List<ReservationFlightEntity>();
    public ICollection<ReservationPassengerEntity> ReservationPassengers { get; set; } = new List<ReservationPassengerEntity>();
    public ICollection<TicketEntity> Tickets { get; set; } = new List<TicketEntity>();
}
