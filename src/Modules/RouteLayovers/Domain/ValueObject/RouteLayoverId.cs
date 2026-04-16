using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;

public sealed class RouteLayoverId
{
    public int Value { get; }

    private RouteLayoverId(int value)
    {
        Value = value;
    }

    public static RouteLayoverId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("RouteLayoverId must be positive", nameof(value));
        return new RouteLayoverId(value);
    }

    public static RouteLayoverId Reconstitute(int value)
    {
        return new RouteLayoverId(value);
    }
}