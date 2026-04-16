using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

public sealed class StaffAvailabilityId
{
    public Guid Value { get; }

    private StaffAvailabilityId(Guid value)
    {
        Value = value;
    }

    public static StaffAvailabilityId Create(Guid value)
    {
        return new StaffAvailabilityId(value);
    }

    public static StaffAvailabilityId Reconstitute(Guid value)
    {
        return new StaffAvailabilityId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is StaffAvailabilityId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}