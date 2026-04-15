using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Aggregate;

public class ReservationStatusTransition
{
    public ReservationStatusTransitionId Id { get; private set; }
    public ReservationStatusTransitionOriginStatusId OriginStatusId { get; private set; }
    public ReservationStatusTransitionDestinationStatusId DestinationStatusId { get; private set; }

    private ReservationStatusTransition(
        ReservationStatusTransitionId id,
        ReservationStatusTransitionOriginStatusId originStatusId,
        ReservationStatusTransitionDestinationStatusId destinationStatusId
    )
    {
        Id = id;
        OriginStatusId = originStatusId;
        DestinationStatusId = destinationStatusId;
    }

    public static ReservationStatusTransition Create(
        ReservationStatusTransitionId id,
        ReservationStatusTransitionOriginStatusId originStatusId,
        ReservationStatusTransitionDestinationStatusId destinationStatusId
    )
    {
        return new ReservationStatusTransition(id, originStatusId, destinationStatusId);
    }
}
