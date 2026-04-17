using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.ValueObject;

public sealed class PositionId
{
    public int Value { get; }

    private PositionId(int value)
    {
        Value = value;
    }

    public static PositionId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("PositionId must be positive");
        return new PositionId(value);
    }

    public static PositionId Reconstitute(int value)
    {
        return new PositionId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is PositionId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}