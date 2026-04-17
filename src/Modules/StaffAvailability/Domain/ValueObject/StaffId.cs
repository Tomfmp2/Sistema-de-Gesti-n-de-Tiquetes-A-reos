using System;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

public sealed class StaffId
{
    public Guid Value { get; }

    private StaffId(Guid value)
    {
        Value = value;
    }

    public static StaffId Create(Guid value)
    {
        return new StaffId(value);
    }

    public static StaffId Reconstitute(Guid value)
    {
        return new StaffId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is StaffId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}