using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.Aggregate;

public sealed class FlightRole
{
    public FlightRoleId Id { get; private set; }
    public FlightRoleName Name { get; private set; }

    private FlightRole(FlightRoleId id, FlightRoleName name)
    {
        Id = id;
        Name = name;
    }

    public static FlightRole Create(FlightRoleName name)
    {
        return new FlightRole(FlightRoleId.Unpersisted, name);
    }

    public static FlightRole Reconstitute(FlightRoleId id, FlightRoleName name)
    {
        return new FlightRole(id, name);
    }

    public FlightRole WithName(FlightRoleName name)
    {
        Name = name;
        return this;
    }
}
