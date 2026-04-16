namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.ValueObject;

public sealed class IsActive
{
    public bool Value { get; }

    private IsActive(bool value)
    {
        Value = value;
    }

    public static IsActive Create(bool value)
    {
        return new IsActive(value);
    }

    public static IsActive Reconstitute(bool value)
    {
        return new IsActive(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is IsActive active && Value == active.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}