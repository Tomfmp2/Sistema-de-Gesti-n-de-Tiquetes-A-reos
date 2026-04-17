using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;

public sealed class AirlineId
{
    public int Value { get; }

    private AirlineId(int value)
    {
        Value = value;
    }

    public static AirlineId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("AirlineId must be positive");
        return new AirlineId(value);
    }

    public static AirlineId Reconstitute(int value)
    {
        return new AirlineId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is AirlineId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}