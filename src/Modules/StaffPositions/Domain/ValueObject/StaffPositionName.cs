using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.ValueObject;

public sealed class StaffPositionName
{
    public string Value { get; }

    private StaffPositionName(string value)
    {
        Value = value;
    }

    public static StaffPositionName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name cannot be empty");
        if (value.Length > 100) throw new ArgumentException("Name cannot exceed 100 characters");
        return new StaffPositionName(value);
    }

    public static StaffPositionName Reconstitute(string value)
    {
        return new StaffPositionName(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is StaffPositionName name && Value == name.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}