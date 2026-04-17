using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

public sealed class MaxTakeoffWeightKg
{
    public decimal Value { get; }

    private MaxTakeoffWeightKg(decimal value)
    {
        Value = value;
    }

    public static MaxTakeoffWeightKg Create(decimal value)
    {
        if (value <= 0) throw new ArgumentException("MaxTakeoffWeightKg must be positive");
        return new MaxTakeoffWeightKg(value);
    }

    public static MaxTakeoffWeightKg Reconstitute(decimal value)
    {
        return new MaxTakeoffWeightKg(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is MaxTakeoffWeightKg weight && Value == weight.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}