namespace sistema_gestor_de_tiquetes_aereos.src.Modules.FlightAssignments.Domain.ValueObject;

public sealed class FlightAssignmentId
{
    public int Value { get; }

    public FlightAssignmentId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("FlightAssignmentId must be greater than zero.", nameof(value));
        
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is FlightAssignmentId other)
            return Value == other.Value;
        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();
}
