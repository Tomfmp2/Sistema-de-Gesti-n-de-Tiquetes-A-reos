using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

public sealed class SeatsPerRow
{
    public int Value { get; }

    private SeatsPerRow(int value)
    {
        Value = value;
    }

    public static SeatsPerRow Create(int value)
    {
        if (value <= 0) throw new ArgumentException("SeatsPerRow must be greater than zero.");
        return new SeatsPerRow(value);
    }

    public static SeatsPerRow Reconstitute(int value)
    {
        return new SeatsPerRow(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is SeatsPerRow seats && Value == seats.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}