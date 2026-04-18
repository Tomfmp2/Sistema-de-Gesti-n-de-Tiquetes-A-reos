namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Domain.ValueObjects;

public sealed class BaggageTypeName : IEquatable<BaggageTypeName>
{
    public string Value { get; }

    private BaggageTypeName(string value)
    {
        Value = value;
    }

    public static BaggageTypeName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("BaggageTypeName cannot be empty.", nameof(value));

        if (value.Length > 50)
            throw new ArgumentException("BaggageTypeName must not exceed 50 characters.", nameof(value));

        return new BaggageTypeName(value.Trim());
    }

    public static BaggageTypeName Reconstitute(string value)
    {
        return new BaggageTypeName(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is BaggageTypeName other && Equals(other);
    }

    public bool Equals(BaggageTypeName? other)
    {
        return other != null && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }
}
