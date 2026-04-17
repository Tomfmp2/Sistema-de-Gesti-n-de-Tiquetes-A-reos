namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

public sealed class Observation
{
    public string Value { get; }

    private Observation(string value)
    {
        Value = value;
    }

    public static Observation Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Observation cannot be null or empty.", nameof(value));
        }
        return new Observation(value);
    }

    public static Observation Reconstitute(string value)
    {
        return new Observation(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Observation observation && Value == observation.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}