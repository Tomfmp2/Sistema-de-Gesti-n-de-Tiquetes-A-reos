namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.ValueObjects;

public sealed class BaggageId
{
    public int Value { get; }

    private BaggageId(int value)
    {
        Value = value;
    }

    public static BaggageId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("BaggageId must be greater than 0.", nameof(value));

        return new BaggageId(value);
    }

    public static BaggageId Reconstitute(int value) => new(value);

    public override bool Equals(object? obj)
    {
        if (obj is not BaggageId other) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();
}
