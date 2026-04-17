using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject;

public sealed class FlightStatusId
{
    public int Value { get; }

    private FlightStatusId(int value)
    {
        Value = value;
    }

    public static FlightStatusId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("FlightStatusId must be positive", nameof(value));
        return new FlightStatusId(value);
    }

    public static FlightStatusId Reconstitute(int value)
    {
        return new FlightStatusId(value);
    }
}