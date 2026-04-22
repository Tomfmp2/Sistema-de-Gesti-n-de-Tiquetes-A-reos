namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

public sealed class StaffAvailabilityId
{
    public int Value { get; }

    private StaffAvailabilityId(int value)
    {
        Value = value;
    }

    public static StaffAvailabilityId Create(int value)
    {
        return new StaffAvailabilityId(value);
    }

    public static StaffAvailabilityId Reconstitute(int value)
    {
        return new StaffAvailabilityId(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is StaffAvailabilityId id && Value == id.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
