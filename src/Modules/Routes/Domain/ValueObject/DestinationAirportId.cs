using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;

public sealed class DestinationAirportId
{
    public int Value { get; }

    private DestinationAirportId(int value)
    {
        Value = value;
    }

    public static DestinationAirportId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("DestinationAirportId must be positive", nameof(value));
        return new DestinationAirportId(value);
    }

    public static DestinationAirportId Reconstitute(int value)
    {
        return new DestinationAirportId(value);
    }
}