using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.ValueObject;

public sealed class IcaoCode
{
    public string? Value { get; }

    private IcaoCode(string? value)
    {
        Value = value;
    }

    public static IcaoCode Create(string? value)
    {
        if (value != null && value.Length != 4) throw new ArgumentException("ICAO code must be 4 characters or null");
        return new IcaoCode(value?.ToUpper());
    }

    public static IcaoCode Reconstitute(string? value)
    {
        return new IcaoCode(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is IcaoCode code && Value == code.Value;
    }

    public override int GetHashCode()
    {
        return Value?.GetHashCode() ?? 0;
    }
}