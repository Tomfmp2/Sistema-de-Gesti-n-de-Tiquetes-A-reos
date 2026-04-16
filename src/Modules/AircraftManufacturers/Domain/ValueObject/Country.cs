namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.ValueObject;

public sealed class Country
{
    public string Value { get; }

    private Country(string value)
    {
        Value = value;
    }

    public static Country Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Country cannot be null or empty.", nameof(value));
        }
        return new Country(value);
    }

    public static Country Reconstitute(string value)
    {
        return new Country(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Country country && Value == country.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}