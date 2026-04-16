using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;

public sealed class SeasonName
{
    public string Value { get; }

    private SeasonName(string value)
    {
        Value = value;
    }

    public static SeasonName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("SeasonName cannot be null or empty", nameof(value));
        if (value.Length > 50)
            throw new ArgumentException("SeasonName cannot exceed 50 characters", nameof(value));
        return new SeasonName(value);
    }

    public static SeasonName Reconstitute(string value)
    {
        return new SeasonName(value);
    }
}