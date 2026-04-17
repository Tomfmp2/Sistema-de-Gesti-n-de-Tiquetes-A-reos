using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

public sealed class AvailabilityStatusId
{
    public Guid Value { get; }

    private AvailabilityStatusId(Guid value)
    {
        Value = value;
    }

    public static AvailabilityStatusId Create(Guid value)
    {
        return new AvailabilityStatusId(value);
    }

    public static AvailabilityStatusId Reconstitute(Guid value)
    {
        return new AvailabilityStatusId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is AvailabilityStatusId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}