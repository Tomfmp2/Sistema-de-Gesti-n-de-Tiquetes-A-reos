using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

public sealed class CabinConfigurationId
{
    public int Value { get; }

    private CabinConfigurationId(int value)
    {
        Value = value;
    }

    public static CabinConfigurationId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("CabinConfigurationId must be positive");
        return new CabinConfigurationId(value);
    }

    public static CabinConfigurationId Reconstitute(int value)
    {
        return new CabinConfigurationId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is CabinConfigurationId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}