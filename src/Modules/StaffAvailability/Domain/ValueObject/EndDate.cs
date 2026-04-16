using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

public sealed class EndDate
{
    public DateTime Value { get; }

    private EndDate(DateTime value)
    {
        Value = value;
    }

    public static EndDate Create(DateTime value)
    {
        return new EndDate(value);
    }

    public static EndDate Reconstitute(DateTime value)
    {
        return new EndDate(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is EndDate date && Value == date.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}