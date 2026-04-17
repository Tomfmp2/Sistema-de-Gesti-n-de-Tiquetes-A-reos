using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;

public sealed class SeasonDescription
{
    public string? Value { get; }

    private SeasonDescription(string? value)
    {
        Value = value;
    }

    public static SeasonDescription Create(string? value)
    {
        if (value != null && value.Length > 150)
            throw new ArgumentException("SeasonDescription cannot exceed 150 characters", nameof(value));
        return new SeasonDescription(value);
    }

    public static SeasonDescription Reconstitute(string? value)
    {
        return new SeasonDescription(value);
    }
}