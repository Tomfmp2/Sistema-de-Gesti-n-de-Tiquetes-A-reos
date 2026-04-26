namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.ValueObject;

public sealed record SeatStatus
{
    public string Value { get; }

    private SeatStatus(string value)
    {
        Value = value;
    }

    public static SeatStatus Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Seat status cannot be empty.", nameof(value));

        return new SeatStatus(value);
    }

    public static SeatStatus Reconstitute(string value)
    {
        return new SeatStatus(value);
    }
}
