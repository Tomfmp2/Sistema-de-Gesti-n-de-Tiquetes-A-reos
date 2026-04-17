using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.ValueObject;

public sealed class AircraftManufacturerId
{
    public int Value { get; }

    private AircraftManufacturerId(int value)
    {
        Value = value;
    }

    public static AircraftManufacturerId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("AircraftManufacturerId must be positive");
        return new AircraftManufacturerId(value);
    }

    public static AircraftManufacturerId Reconstitute(int value)
    {
        return new AircraftManufacturerId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is AircraftManufacturerId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}