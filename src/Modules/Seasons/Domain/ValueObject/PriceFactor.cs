using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;

public sealed class PriceFactor
{
    public decimal Value { get; }

    private PriceFactor(decimal value)
    {
        Value = value;
    }

    public static PriceFactor Create(decimal value)
    {
        if (value < 0 || value > 10)
            throw new ArgumentException("PriceFactor must be between 0 and 10", nameof(value));
        return new PriceFactor(value);
    }

    public static PriceFactor Reconstitute(decimal value)
    {
        return new PriceFactor(value);
    }
}