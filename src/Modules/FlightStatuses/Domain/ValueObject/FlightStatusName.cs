using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject;

public sealed class FlightStatusName
{
    public string Value { get; }

    private FlightStatusName(string value)
    {
        Value = value;
    }

    public static FlightStatusName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("FlightStatusName cannot be null or empty", nameof(value));
        if (value.Length > 50)
            throw new ArgumentException("FlightStatusName cannot exceed 50 characters", nameof(value));
        return new FlightStatusName(value);
    }

    public static FlightStatusName Reconstitute(string value)
    {
        return new FlightStatusName(value);
    }
}