using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

public sealed class MaxCapacity
{
    public int Value { get; }

    private MaxCapacity(int value)
    {
        Value = value;
    }

    public static MaxCapacity Create(int value)
    {
        if (value <= 0) throw new ArgumentException("MaxCapacity must be positive");
        return new MaxCapacity(value);
    }

    public static MaxCapacity Reconstitute(int value)
    {
        return new MaxCapacity(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is MaxCapacity capacity && Value == capacity.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}