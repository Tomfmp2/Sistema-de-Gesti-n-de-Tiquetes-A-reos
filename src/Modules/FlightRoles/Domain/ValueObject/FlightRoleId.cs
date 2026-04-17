namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.ValueObject;

public sealed class FlightRoleId
{
    public int Value { get; }

    private FlightRoleId(int value)
    {
        Value = value;
    }

    public static FlightRoleId Unpersisted => new(0);

    public static FlightRoleId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "FlightRoleId debe ser mayor que 0.");
        }

        return new FlightRoleId(value);
    }
}
