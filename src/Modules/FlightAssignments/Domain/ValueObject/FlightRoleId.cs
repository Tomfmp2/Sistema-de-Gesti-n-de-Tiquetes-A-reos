namespace sistema_gestor_de_tiquetes_aereos.src.Modules.FlightAssignments.Domain.ValueObject;

public sealed class FlightRoleId
{
    public int Value { get; }

    public FlightRoleId(int value)
    {
        if (value <= 0)
            throw new ArgumentException("FlightRoleId must be greater than zero.", nameof(value));
        
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is FlightRoleId other)
            return Value == other.Value;
        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();
}
