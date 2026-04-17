namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.ValueObject;

public sealed class CabinTypeId
{
    public int Value { get; }

    public CabinTypeId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("CabinTypeId must be greater than zero.", nameof(value));
        
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is CabinTypeId other)
            return Value == other.Value;
        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();
}
