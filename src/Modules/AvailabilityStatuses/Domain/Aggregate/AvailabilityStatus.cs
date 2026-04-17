using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.Aggregate;

public sealed class AvailabilityStatus
{
    public AvailabilityStatusId Id { get; private set; }
    public AvailabilityStatusName Name { get; private set; }

    private AvailabilityStatus(AvailabilityStatusId id, AvailabilityStatusName name)
    {
        Id = id;
        Name = name;
    }

    public static AvailabilityStatus Create(AvailabilityStatusId id, AvailabilityStatusName name)
    {
        return new AvailabilityStatus(id, name);
    }

    public static AvailabilityStatus Reconstitute(AvailabilityStatusId id, AvailabilityStatusName name)
    {
        return new AvailabilityStatus(id, name);
    }

    public void UpdateName(AvailabilityStatusName name)
    {
        Name = name;
    }
}