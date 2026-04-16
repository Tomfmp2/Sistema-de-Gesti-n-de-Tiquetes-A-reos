namespace sistema_gestor_de_tiquetes_aereos.src.Modules.FlightAssignments.Domain.ValueObject;

public sealed class StaffId
{
    public int Value { get; }

    public StaffId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("StaffId must be greater than zero.", nameof(value));
        
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is StaffId other)
            return Value == other.Value;
        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();
}
