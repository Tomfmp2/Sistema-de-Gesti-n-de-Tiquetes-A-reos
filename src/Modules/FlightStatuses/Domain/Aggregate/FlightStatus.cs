using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;

public class FlightStatus
{
    public FlightStatusId Id { get; private set; }
    public FlightStatusName Name { get; private set; }

    private FlightStatus(
        FlightStatusId id,
        FlightStatusName name
    )
    {
        Id = id;
        Name = name;
    }

    public static FlightStatus Create(
        FlightStatusId id,
        FlightStatusName name
    )
    {
        return new FlightStatus(
            id,
            name
        );
    }
}
