using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.ValueObject;

public sealed class AirportAirlineId
{
    public int Value { get; }

    private AirportAirlineId(int value)
    {
        Value = value;
    }

    public static AirportAirlineId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("Id must be positive");
        return new AirportAirlineId(value);
    }

    public static AirportAirlineId Reconstitute(int value)
    {
        return new AirportAirlineId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is AirportAirlineId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}