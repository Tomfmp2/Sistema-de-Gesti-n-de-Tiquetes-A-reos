using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;

public sealed class LayoverDurationMin
{
    public int Value { get; }

    private LayoverDurationMin(int value)
    {
        Value = value;
    }

    public static LayoverDurationMin Create(int value)
    {
        if (value < 0)
            throw new ArgumentException("LayoverDurationMin must be non-negative", nameof(value));
        return new LayoverDurationMin(value);
    }

    public static LayoverDurationMin Reconstitute(int value)
    {
        return new LayoverDurationMin(value);
    }
}