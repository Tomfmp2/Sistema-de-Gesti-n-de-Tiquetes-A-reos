using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.ValueObject;

public sealed class IataCode
{
    public string Value { get; }

    private IataCode(string value)
    {
        Value = value;
    }

    public static IataCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("IATA code cannot be empty");
        // Airline IATA codes are commonly 2 characters, but some may be 3.
        if (value.Length is < 2 or > 3) throw new ArgumentException("IATA code must be 2 or 3 characters");
        return new IataCode(value.ToUpper());
    }

    public static IataCode Reconstitute(string value)
    {
        return new IataCode(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is IataCode code && Value == code.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}