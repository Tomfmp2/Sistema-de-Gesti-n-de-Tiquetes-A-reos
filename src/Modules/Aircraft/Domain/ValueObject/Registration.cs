namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;

public sealed class Registration
{
    public string Value { get; }

    private Registration(string value)
    {
        Value = value;
    }

    public static Registration Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Registration cannot be null or empty.", nameof(value));
        }
        return new Registration(value);
    }

    public static Registration Reconstitute(string value)
    {
        return new Registration(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Registration registration && Value == registration.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}