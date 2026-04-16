namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

public sealed class ModelName
{
    public string Value { get; }

    private ModelName(string value)
    {
        Value = value;
    }

    public static ModelName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("ModelName cannot be null or empty.", nameof(value));
        }
        return new ModelName(value);
    }

    public static ModelName Reconstitute(string value)
    {
        return new ModelName(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is ModelName name && Value == name.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}