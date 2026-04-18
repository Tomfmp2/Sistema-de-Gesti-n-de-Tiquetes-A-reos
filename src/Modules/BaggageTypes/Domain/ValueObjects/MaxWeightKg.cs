namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Domain.ValueObjects;

public sealed class MaxWeightKg : IEquatable<MaxWeightKg>
{
    public decimal Value { get; }

    private MaxWeightKg(decimal value)
    {
        Value = value;
    }

    public static MaxWeightKg Create(decimal value)
    {
        if (value <= 0)
            throw new ArgumentException("MaxWeightKg must be greater than zero.", nameof(value));

        return new MaxWeightKg(value);
    }

    public static MaxWeightKg Reconstitute(decimal value)
    {
        return new MaxWeightKg(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is MaxWeightKg other && Equals(other);
    }

    public bool Equals(MaxWeightKg? other)
    {
        return other != null && Value == other.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
