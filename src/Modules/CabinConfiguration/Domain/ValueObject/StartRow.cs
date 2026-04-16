using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

public sealed class StartRow
{
    public int Value { get; }

    private StartRow(int value)
    {
        Value = value;
    }

    public static StartRow Create(int value)
    {
        if (value <= 0) throw new ArgumentException("StartRow must be greater than zero.");
        return new StartRow(value);
    }

    public static StartRow Reconstitute(int value)
    {
        return new StartRow(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is StartRow row && Value == row.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}