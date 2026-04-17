using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;

public sealed class SequenceOrder
{
    public int Value { get; }

    private SequenceOrder(int value)
    {
        Value = value;
    }

    public static SequenceOrder Create(int value)
    {
        if (value < 0)
            throw new ArgumentException("SequenceOrder must be non-negative", nameof(value));
        return new SequenceOrder(value);
    }

    public static SequenceOrder Reconstitute(int value)
    {
        return new SequenceOrder(value);
    }
}