using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.Aggregate;

public sealed class SeatLocationType
{
    public SeatLocationTypeId Id { get; }
    public SeatLocationTypeName Name { get; }

    private SeatLocationType(SeatLocationTypeId id, SeatLocationTypeName name)
    {
        Id = id;
        Name = name;
    }

    public static SeatLocationType Create(SeatLocationTypeName name)
    {
        return new SeatLocationType(SeatLocationTypeId.Reconstitute(0), name);
    }

    public static SeatLocationType Reconstitute(SeatLocationTypeId id, SeatLocationTypeName name)
    {
        return new SeatLocationType(id, name);
    }
}