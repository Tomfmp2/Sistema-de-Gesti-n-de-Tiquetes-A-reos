using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.Aggregate;

public sealed class StaffPosition
{
    public StaffPositionId Id { get; private set; }
    public StaffPositionName Name { get; private set; }

    private StaffPosition(StaffPositionId id, StaffPositionName name)
    {
        Id = id;
        Name = name;
    }

    public static StaffPosition Create(StaffPositionId id, StaffPositionName name)
    {
        return new StaffPosition(id, name);
    }

    public static StaffPosition Reconstitute(StaffPositionId id, StaffPositionName name)
    {
        return new StaffPosition(id, name);
    }

    public void UpdateName(StaffPositionName name)
    {
        Name = name;
    }
}