namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.ValueObjects;

public sealed class WeightKg
{
    public decimal Value { get; }

    private WeightKg(decimal value)
    {
        Value = value;
    }

    public static WeightKg Create(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("WeightKg must be greater than or equal to 0.", nameof(value));

        return new WeightKg(value);
    }

    public static WeightKg Reconstitute(decimal value) => new(value);

    public override bool Equals(object? obj)
    {
        if (obj is not WeightKg other) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString("F2");
}
