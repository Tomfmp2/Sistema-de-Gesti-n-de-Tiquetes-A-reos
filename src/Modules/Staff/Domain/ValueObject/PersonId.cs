using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.ValueObject;

public sealed class PersonId
{
    public int Value { get; }

    private PersonId(int value)
    {
        Value = value;
    }

    public static PersonId Create(int value)
    {
        if (value <= 0) throw new ArgumentException("PersonId must be positive");
        return new PersonId(value);
    }

    public static PersonId Reconstitute(int value)
    {
        return new PersonId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is PersonId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}