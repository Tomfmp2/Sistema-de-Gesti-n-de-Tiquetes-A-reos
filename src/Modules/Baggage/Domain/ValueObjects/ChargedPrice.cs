namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.ValueObjects;

public sealed class ChargedPrice
{
    public decimal Value { get; }

    private ChargedPrice(decimal value)
    {
        Value = value;
    }

    public static ChargedPrice Create(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("ChargedPrice must be greater than or equal to 0.", nameof(value));

        return new ChargedPrice(value);
    }

    public static ChargedPrice Reconstitute(decimal value) => new(value);

    public override bool Equals(object? obj)
    {
        if (obj is not ChargedPrice other) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString("F2");
}
