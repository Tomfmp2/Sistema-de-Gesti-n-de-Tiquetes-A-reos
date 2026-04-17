using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Aggregate;

public class Ticket
{
    public TicketId Id { get; private set; }
    public TicketReservationPassengerId ReservationPassengerId { get; private set; }
    public TicketCode Code { get; private set; }
    public TicketIssueDate IssueDate { get; private set; }
    public TicketStatusId TicketStatusId { get; private set; }
    public TicketCreatedAt CreatedAt { get; private set; }
    public TicketUpdatedAt UpdatedAt { get; private set; }

    private Ticket(
        TicketId id,
        TicketReservationPassengerId reservationPassengerId,
        TicketCode code,
        TicketIssueDate issueDate,
        TicketStatusId ticketStatusId,
        TicketCreatedAt createdAt,
        TicketUpdatedAt updatedAt
    )
    {
        Id = id;
        ReservationPassengerId = reservationPassengerId;
        Code = code;
        IssueDate = issueDate;
        TicketStatusId = ticketStatusId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Ticket Create(
        TicketId id,
        TicketReservationPassengerId reservationPassengerId,
        TicketCode code,
        TicketIssueDate issueDate,
        TicketStatusId ticketStatusId,
        TicketCreatedAt createdAt,
        TicketUpdatedAt updatedAt
    )
    {
        return new Ticket(
            id,
            reservationPassengerId,
            code,
            issueDate,
            ticketStatusId,
            createdAt,
            updatedAt
        );
    }
}
