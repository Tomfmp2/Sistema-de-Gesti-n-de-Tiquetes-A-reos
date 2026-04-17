using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

public sealed class FuelConsumptionKgH
{
    public decimal Value { get; }

    private FuelConsumptionKgH(decimal value)
    {
        Value = value;
    }

    public static FuelConsumptionKgH Create(decimal value)
    {
        if (value <= 0) throw new ArgumentException("FuelConsumptionKgH must be positive");
        return new FuelConsumptionKgH(value);
    }

    public static FuelConsumptionKgH Reconstitute(decimal value)
    {
        return new FuelConsumptionKgH(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is FuelConsumptionKgH consumption && Value == consumption.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}