namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;

public sealed class ManufacturingDate
{
    public DateOnly Value { get; }

    private ManufacturingDate(DateOnly value)
    {
        Value = value;
    }

    public static ManufacturingDate Create(DateOnly value)
    {
        return new ManufacturingDate(value);
    }

    public static ManufacturingDate Reconstitute(DateOnly value)
    {
        return new ManufacturingDate(value);
    }

    public override bool Equals(object? obj)
    {
        return obj is ManufacturingDate date && Value == date.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}