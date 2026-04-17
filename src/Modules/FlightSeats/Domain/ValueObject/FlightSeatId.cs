namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.ValueObject;

public sealed class FlightSeatId
{
    public int Value { get; }

    public FlightSeatId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("FlightSeatId must be greater than zero.", nameof(value));
        
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is FlightSeatId other)
            return Value == other.Value;
        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();
}
