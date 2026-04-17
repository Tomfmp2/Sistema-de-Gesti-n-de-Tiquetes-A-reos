using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

public sealed class SeatLetters
{
    public string Value { get; }

    private SeatLetters(string value)
    {
        Value = value;
    }

    public static SeatLetters Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("SeatLetters cannot be null or empty.", nameof(value));
        }

        if (value.Length > 10)
        {
            throw new ArgumentException("SeatLetters cannot exceed 10 characters.", nameof(value));
        }

        return new SeatLetters(value);
    }

    public static SeatLetters Reconstitute(string value)
    {
        return new SeatLetters(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is SeatLetters letters && Value == letters.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}