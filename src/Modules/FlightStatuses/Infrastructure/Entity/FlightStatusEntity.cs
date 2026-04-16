using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;

public class FlightStatusEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public static FlightStatusEntity FromDomain(FlightStatus flightStatus)
    {
        return new FlightStatusEntity
        {
            Id = flightStatus.Id?.Value ?? 0,
            Name = flightStatus.Name.Value
        };
    }

    public FlightStatus ToDomain()
    {
        return FlightStatus.Reconstitute(
            sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject.FlightStatusId.Reconstitute(Id),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject.FlightStatusName.Reconstitute(Name)
        );
    }
}