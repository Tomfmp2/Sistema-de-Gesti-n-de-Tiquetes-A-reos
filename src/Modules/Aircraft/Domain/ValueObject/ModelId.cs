using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;

public sealed class ModelId
{
    public int Value { get; }

    private ModelId(int value)
    {
        Value = value;
    }

    public static ModelId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("ModelId must be positive");
        return new ModelId(value);
    }

    public static ModelId Reconstitute(int value)
    {
        return new ModelId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is ModelId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}