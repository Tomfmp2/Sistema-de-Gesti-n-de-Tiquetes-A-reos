using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;

public sealed class RouteId
{
    public int Value { get; }

    private RouteId(int value)
    {
        Value = value;
    }

    public static RouteId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("RouteId must be positive", nameof(value));
        return new RouteId(value);
    }

    public static RouteId Reconstitute(int value)
    {
        return new RouteId(value);
    }
}