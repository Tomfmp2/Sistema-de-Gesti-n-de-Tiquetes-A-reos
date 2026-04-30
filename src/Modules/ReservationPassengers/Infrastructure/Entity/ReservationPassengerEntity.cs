using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;

public class ReservationPassengerEntity
{
    public int Id { get; set; }
    public int ReservationFlightId { get; set; }
    public int PassengerId { get; set; }
    public int CabinTypeId { get; set; }

    public ReservationFlightEntity? ReservationFlight { get; set; }
    public PassengerEntity? Passenger { get; set; }
    public CabinTypeEntity? CabinType { get; set; }
    public ICollection<TicketEntity> Tickets { get; set; } = new List<TicketEntity>();
    public ICollection<InvoiceItemEntity> InvoiceItems { get; set; } = new List<InvoiceItemEntity>();
}
