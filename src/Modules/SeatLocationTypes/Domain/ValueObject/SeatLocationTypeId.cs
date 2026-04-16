using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;

public sealed class SeatLocationTypeId
{
    public int Value { get; }

    private SeatLocationTypeId(int value)
    {
        Value = value;
    }

    public static SeatLocationTypeId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("SeatLocationTypeId must be positive", nameof(value));
        return new SeatLocationTypeId(value);
    }

    public static SeatLocationTypeId Reconstitute(int value)
    {
        return new SeatLocationTypeId(value);
    }
}