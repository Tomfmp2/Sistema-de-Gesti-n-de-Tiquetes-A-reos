namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Domain.ValueObjects;

public sealed class BaggageTypeId : IEquatable<BaggageTypeId>
{
    public int Value { get; }

    private BaggageTypeId(int value)
    {
        Value = value;
    }

    public static BaggageTypeId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("BaggageTypeId must be greater than zero.", nameof(value));

        return new BaggageTypeId(value);
    }

    public static BaggageTypeId Reconstitute(int value)
    {
        return new BaggageTypeId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is BaggageTypeId other && Equals(other);
    }

    public bool Equals(BaggageTypeId? other)
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
