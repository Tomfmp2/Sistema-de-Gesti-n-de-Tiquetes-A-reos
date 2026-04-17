using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.ValueObject;

public sealed class AirlineName
{
    public string Value { get; }

    private AirlineName(string value)
    {
        Value = value;
    }

    public static AirlineName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name cannot be empty");
        if (value.Length > 150) throw new ArgumentException("Name too long");
        return new AirlineName(value);
    }

    public static AirlineName Reconstitute(string value)
    {
        return new AirlineName(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is AirlineName name && Value == name.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}