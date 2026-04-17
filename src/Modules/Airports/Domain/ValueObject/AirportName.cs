using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.ValueObject;

public sealed class AirportName
{
    public string Value { get; }

    private AirportName(string value)
    {
        Value = value;
    }

    public static AirportName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name cannot be empty");
        if (value.Length > 150) throw new ArgumentException("Name too long");
        return new AirportName(value);
    }

    public static AirportName Reconstitute(string value)
    {
        return new AirportName(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is AirportName name && Value == name.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}