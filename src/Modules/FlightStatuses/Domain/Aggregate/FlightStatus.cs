using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;

public sealed class FlightStatus
{
    public FlightStatusId Id { get; }
    public FlightStatusName Name { get; }

    private FlightStatus(FlightStatusId id, FlightStatusName name)
    {
        Id = id;
        Name = name;
    }

    public static FlightStatus Create(FlightStatusName name)
    {
        return new FlightStatus(null, name);
    }

    public static FlightStatus Reconstitute(FlightStatusId id, FlightStatusName name)
    {
        return new FlightStatus(id, name);
    }
}