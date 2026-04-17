namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.ValueObject;

public sealed class SeatCode
{
    public string Value { get; }

    public SeatCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("SeatCode cannot be empty.", nameof(value));
        
        if (value.Length > 5)
            throw new ArgumentException("SeatCode cannot exceed 5 characters.", nameof(value));
        
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is SeatCode other)
            return Value == other.Value;
        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value;
}
