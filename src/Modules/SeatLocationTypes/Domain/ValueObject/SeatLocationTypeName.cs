using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;

public sealed class SeatLocationTypeName
{
    public string Value { get; }

    private SeatLocationTypeName(string value)
    {
        Value = value;
    }

    public static SeatLocationTypeName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("SeatLocationTypeName cannot be null or empty", nameof(value));
        if (value.Length > 50)
            throw new ArgumentException("SeatLocationTypeName cannot exceed 50 characters", nameof(value));
        return new SeatLocationTypeName(value);
    }

    public static SeatLocationTypeName Reconstitute(string value)
    {
        return new SeatLocationTypeName(value);
    }
}