using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

public sealed class StartDate
{
    public DateTime Value { get; }

    private StartDate(DateTime value)
    {
        Value = value;
    }

    public static StartDate Create(DateTime value)
    {
        return new StartDate(value);
    }

    public static StartDate Reconstitute(DateTime value)
    {
        return new StartDate(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is StartDate date && Value == date.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}