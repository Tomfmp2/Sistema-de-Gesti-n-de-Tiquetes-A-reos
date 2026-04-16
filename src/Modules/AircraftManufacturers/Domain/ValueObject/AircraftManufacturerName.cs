namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.ValueObject;

public sealed class AircraftManufacturerName
{
    public string Value { get; }

    private AircraftManufacturerName(string value)
    {
        Value = value;
    }

    public static AircraftManufacturerName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("AircraftManufacturerName cannot be null or empty.", nameof(value));
        }
        return new AircraftManufacturerName(value);
    }

    public static AircraftManufacturerName Reconstitute(string value)
    {
        return new AircraftManufacturerName(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is AircraftManufacturerName name && Value == name.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}