using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;

public sealed class OriginAirportId
{
    public int Value { get; }

    private OriginAirportId(int value)
    {
        Value = value;
    }

    public static OriginAirportId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("OriginAirportId must be positive", nameof(value));
        return new OriginAirportId(value);
    }

    public static OriginAirportId Reconstitute(int value)
    {
        return new OriginAirportId(value);
    }
}