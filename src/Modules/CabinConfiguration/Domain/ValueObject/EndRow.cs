using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

public sealed class EndRow
{
    public int Value { get; }

    private EndRow(int value)
    {
        Value = value;
    }

    public static EndRow Create(int value)
    {
        if (value <= 0) throw new ArgumentException("EndRow must be greater than zero.");
        return new EndRow(value);
    }

    public static EndRow Reconstitute(int value)
    {
        return new EndRow(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is EndRow row && Value == row.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}