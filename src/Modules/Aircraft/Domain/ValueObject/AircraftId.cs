using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;

public sealed class AircraftId
{
    public int Value { get; }

    private AircraftId(int value)
    {
        Value = value;
    }

    public static AircraftId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("AircraftId must be positive");
        return new AircraftId(value);
    }

    public static AircraftId Reconstitute(int value)
    {
        return new AircraftId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is AircraftId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}