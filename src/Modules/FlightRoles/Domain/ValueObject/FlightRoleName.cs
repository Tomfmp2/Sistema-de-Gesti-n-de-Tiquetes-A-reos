namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.ValueObject;

public sealed class FlightRoleName
{
    public string Value { get; }

    private FlightRoleName(string value)
    {
        Value = value;
    }

    public static FlightRoleName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Flight role name is required.", nameof(value));
        }

        var normalized = value.Trim();

        if (normalized.Length > 100)
        {
            throw new ArgumentException("Flight role name must be 100 characters or fewer.", nameof(value));
        }

        return new FlightRoleName(normalized);
    }
}
