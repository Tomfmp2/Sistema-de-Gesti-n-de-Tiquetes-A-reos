namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject;

public sealed class CabinTypeName
{
    public string Value { get; }

    private CabinTypeName(string value)
    {
        Value = value;
    }

    public static CabinTypeName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("CabinTypeName cannot be null or empty.", nameof(value));
        }
        if (value.Length > 50)
        {
            throw new ArgumentException("CabinTypeName cannot exceed 50 characters.", nameof(value));
        }
        return new CabinTypeName(value);
    }

    public static CabinTypeName Reconstitute(string value)
    {
        return new CabinTypeName(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is CabinTypeName name && Value == name.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}