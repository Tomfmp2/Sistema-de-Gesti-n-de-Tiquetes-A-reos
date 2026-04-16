using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

public sealed class CabinTypeId
{
    public int Value { get; }

    private CabinTypeId(int value)
    {
        Value = value;
    }

    public static CabinTypeId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("CabinTypeId must be positive");
        return new CabinTypeId(value);
    }

    public static CabinTypeId Reconstitute(int value)
    {
        return new CabinTypeId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is CabinTypeId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}