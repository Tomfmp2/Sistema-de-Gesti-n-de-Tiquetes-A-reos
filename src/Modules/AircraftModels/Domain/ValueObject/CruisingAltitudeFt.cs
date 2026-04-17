using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

public sealed class CruisingAltitudeFt
{
    public int Value { get; }

    private CruisingAltitudeFt(int value)
    {
        Value = value;
    }

    public static CruisingAltitudeFt Create(int value)
    {
        if (value <= 0) throw new ArgumentException("CruisingAltitudeFt must be positive");
        return new CruisingAltitudeFt(value);
    }

    public static CruisingAltitudeFt Reconstitute(int value)
    {
        return new CruisingAltitudeFt(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is CruisingAltitudeFt altitude && Value == altitude.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}