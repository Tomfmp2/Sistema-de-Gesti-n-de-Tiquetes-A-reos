using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.ValueObject;

public sealed class AirportId
{
    public int Value { get; }

    private AirportId(int value)
    {
        Value = value;
    }

    public static AirportId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("Id must be positive");
        return new AirportId(value);
    }

    public static AirportId Reconstitute(int value)
    {
        return new AirportId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is AirportId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}