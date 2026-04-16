using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.ValueObject;

public sealed class Terminal
{
    public string? Value { get; }

    private Terminal(string? value)
    {
        Value = value;
    }

    public static Terminal Create(string? value)
    {
        if (value != null && value.Length > 20) throw new ArgumentException("Terminal too long");
        return new Terminal(value);
    }

    public static Terminal Reconstitute(string? value)
    {
        return new Terminal(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Terminal terminal && Value == terminal.Value;
    }

    public override int GetHashCode()
    {
        return Value?.GetHashCode() ?? 0;
    }
}