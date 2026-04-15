using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Aggregate;

public class Reservation
{
    public ReservationId Id { get; private set; }
    public ReservationClientId ClientId { get; private set; }
    public ReservationDate ReservationDate { get; private set; }
    public ReservationStatusId ReservationStatusId { get; private set; }
    public ReservationTotalValue TotalValue { get; private set; }
    public ReservationExpiresAt ExpiresAt { get; private set; }
    public ReservationCreatedAt CreatedAt { get; private set; }
    public ReservationUpdatedAt UpdatedAt { get; private set; }

    private Reservation(
        ReservationId id,
        ReservationClientId clientId,
        ReservationDate reservationDate,
        ReservationStatusId reservationStatusId,
        ReservationTotalValue totalValue,
        ReservationExpiresAt expiresAt,
        ReservationCreatedAt createdAt,
        ReservationUpdatedAt updatedAt
    )
    {
        Id = id;
        ClientId = clientId;
        ReservationDate = reservationDate;
        ReservationStatusId = reservationStatusId;
        TotalValue = totalValue;
        ExpiresAt = expiresAt;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Reservation Create(
        ReservationId id,
        ReservationClientId clientId,
        ReservationDate reservationDate,
        ReservationStatusId reservationStatusId,
        ReservationTotalValue totalValue,
        ReservationExpiresAt expiresAt,
        ReservationCreatedAt createdAt,
        ReservationUpdatedAt updatedAt
    )
    {
        return new Reservation(
            id,
            clientId,
            reservationDate,
            reservationStatusId,
            totalValue,
            expiresAt,
            createdAt,
            updatedAt
        );
    }
}
