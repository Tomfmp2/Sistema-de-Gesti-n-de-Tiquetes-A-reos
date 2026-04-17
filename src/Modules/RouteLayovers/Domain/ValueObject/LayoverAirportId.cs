using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;

public sealed class LayoverAirportId
{
    public int Value { get; }

    private LayoverAirportId(int value)
    {
        Value = value;
    }

    public static LayoverAirportId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("LayoverAirportId must be positive", nameof(value));
        return new LayoverAirportId(value);
    }

    public static LayoverAirportId Reconstitute(int value)
    {
        return new LayoverAirportId(value);
    }
}