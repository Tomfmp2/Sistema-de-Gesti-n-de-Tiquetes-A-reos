using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

public sealed class ManufacturerId
{
    public int Value { get; }

    private ManufacturerId(int value)
    {
        Value = value;
    }

    public static ManufacturerId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("ManufacturerId must be positive");
        return new ManufacturerId(value);
    }

    public static ManufacturerId Reconstitute(int value)
    {
        return new ManufacturerId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is ManufacturerId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}