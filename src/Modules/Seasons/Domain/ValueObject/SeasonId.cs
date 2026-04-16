using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;

public sealed class SeasonId
{
    public int Value { get; }

    private SeasonId(int value)
    {
        Value = value;
    }

    public static SeasonId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("SeasonId must be positive", nameof(value));
        return new SeasonId(value);
    }

    public static SeasonId Reconstitute(int value)
    {
        return new SeasonId(value);
    }
}