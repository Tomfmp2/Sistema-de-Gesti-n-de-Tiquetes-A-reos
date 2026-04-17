using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

public sealed class AircraftModelId
{
    public int Value { get; }

    private AircraftModelId(int value)
    {
        Value = value;
    }

    public static AircraftModelId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("AircraftModelId must be positive");
        return new AircraftModelId(value);
    }

    public static AircraftModelId Reconstitute(int value)
    {
        return new AircraftModelId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is AircraftModelId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}