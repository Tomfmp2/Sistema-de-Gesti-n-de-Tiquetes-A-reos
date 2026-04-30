using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;

public sealed class RouteMiles
{
    public decimal Value { get; }

    private RouteMiles(decimal value)
    {
        Value = value;
    }

    public static RouteMiles Create(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Miles cannot be negative", nameof(value));
        return new RouteMiles(value);
    }

    public static RouteMiles Reconstitute(decimal value)
    {
        return new RouteMiles(value);
    }
}
