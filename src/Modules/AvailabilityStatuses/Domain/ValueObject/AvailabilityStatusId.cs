using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.ValueObject;

public sealed class AvailabilityStatusId
{
    public int Value { get; }

    private AvailabilityStatusId(int value)
    {
        Value = value;
    }

    public static AvailabilityStatusId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("Id must be positive");
        return new AvailabilityStatusId(value);
    }

    public static AvailabilityStatusId Reconstitute(int value)
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