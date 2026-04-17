using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

public sealed class StaffId
{
    public int Value { get; }

    private StaffId(int value)
    {
        Value = value;
    }

    public static StaffId Create(int value)
    {
        return new StaffId(value);
    }

    public static StaffId Reconstitute(int value)
    {
        return new StaffId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is StaffId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}