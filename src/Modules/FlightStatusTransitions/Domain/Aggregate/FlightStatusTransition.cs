using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Aggregate;

public class FlightStatusTransition
{
    public FlightStatusTransitionId Id { get; private set; }
    public FlightStatusTransitionOriginStatusId OriginStatusId { get; private set; }
    public FlightStatusTransitionDestinationStatusId DestinationStatusId { get; private set; }

    private FlightStatusTransition(
        FlightStatusTransitionId id,
        FlightStatusTransitionOriginStatusId originStatusId,
        FlightStatusTransitionDestinationStatusId destinationStatusId
    )
    {
        Id = id;
        OriginStatusId = originStatusId;
        DestinationStatusId = destinationStatusId;
    }

    public static FlightStatusTransition Create(
        FlightStatusTransitionId id,
        FlightStatusTransitionOriginStatusId originStatusId,
        FlightStatusTransitionDestinationStatusId destinationStatusId
    )
    {
        return new FlightStatusTransition(
            id,
            originStatusId,
            destinationStatusId
        );
    }
}
