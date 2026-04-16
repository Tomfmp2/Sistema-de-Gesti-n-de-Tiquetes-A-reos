using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.ValueObject;

public sealed class HireDate
{
    public DateOnly Value { get; }

    private HireDate(DateOnly value)
    {
        Value = value;
    }

    public static HireDate Create(DateOnly value)
    {
        if (value > DateOnly.FromDateTime(DateTime.Now)) throw new ArgumentException("Hire date cannot be in the future");
        return new HireDate(value);
    }

    public static HireDate Reconstitute(DateOnly value)
    {
        return new HireDate(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is HireDate date && Value == date.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}