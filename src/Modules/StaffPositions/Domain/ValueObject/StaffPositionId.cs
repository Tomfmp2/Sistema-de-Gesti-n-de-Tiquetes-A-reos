using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.ValueObject;

public sealed class StaffPositionId
{
    public int Value { get; }

    private StaffPositionId(int value)
    {
        Value = value;
    }

    public static StaffPositionId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("Id must be positive");
        return new StaffPositionId(value);
    }

    public static StaffPositionId Reconstitute(int value)
    {
        return new StaffPositionId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is StaffPositionId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}