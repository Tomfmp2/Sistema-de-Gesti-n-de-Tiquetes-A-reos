namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Domain.ValueObjects;

public sealed class BasePrice : IEquatable<BasePrice>
{
    public decimal Value { get; }

    private BasePrice(decimal value)
    {
        Value = value;
    }

    public static BasePrice Create(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("BasePrice cannot be negative.", nameof(value));

        return new BasePrice(value);
    }

    public static BasePrice Reconstitute(decimal value)
    {
        return new BasePrice(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is BasePrice other && Equals(other);
    }

    public bool Equals(BasePrice? other)
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
