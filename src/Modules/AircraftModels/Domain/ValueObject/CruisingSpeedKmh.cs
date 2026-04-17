using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

public sealed class CruisingSpeedKmh
{
    public int Value { get; }

    private CruisingSpeedKmh(int value)
    {
        Value = value;
    }

    public static CruisingSpeedKmh Create(int value)
    {
        if (value <= 0) throw new ArgumentException("CruisingSpeedKmh must be positive");
        return new CruisingSpeedKmh(value);
    }

    public static CruisingSpeedKmh Reconstitute(int value)
    {
        return new CruisingSpeedKmh(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is CruisingSpeedKmh speed && Value == speed.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}