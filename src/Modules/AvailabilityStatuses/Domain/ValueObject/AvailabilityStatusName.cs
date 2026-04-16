using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.ValueObject;

public sealed class AvailabilityStatusName
{
    public string Value { get; }

    private AvailabilityStatusName(string value)
    {
        Value = value;
    }

    public static AvailabilityStatusName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name cannot be empty");
        if (value.Length > 50) throw new ArgumentException("Name cannot exceed 50 characters");
        return new AvailabilityStatusName(value);
    }

    public static AvailabilityStatusName Reconstitute(string value)
    {
        return new AvailabilityStatusName(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is AvailabilityStatusName name && Value == name.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}