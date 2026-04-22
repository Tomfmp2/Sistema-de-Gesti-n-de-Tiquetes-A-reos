using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Aggregate;

public sealed class AirportAirlineRecord
{
    public AirportAirlineId Id { get; internal set; }
    public int AirportId { get; private set; }
    public int AirlineId { get; private set; }
    public Terminal Terminal { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly? EndDate { get; private set; }
    public bool IsActive { get; private set; }

    private AirportAirlineRecord(AirportAirlineId id, int airportId, int airlineId, Terminal terminal, DateOnly startDate, DateOnly? endDate, bool isActive)
    {
        Id = id;
        AirportId = airportId;
        AirlineId = airlineId;
        Terminal = terminal;
        StartDate = startDate;
        EndDate = endDate;
        IsActive = isActive;
    }

    public static AirportAirlineRecord Create(int airportId, int airlineId, Terminal terminal, DateOnly startDate, DateOnly? endDate)
    {
        return new AirportAirlineRecord(
            AirportAirlineId.Reconstitute(0), // will be set by DB
            airportId,
            airlineId,
            terminal,
            startDate,
            endDate,
            true
        );
    }

    public static AirportAirlineRecord Reconstitute(AirportAirlineId id, int airportId, int airlineId, Terminal terminal, DateOnly startDate, DateOnly? endDate, bool isActive)
    {
        return new AirportAirlineRecord(id, airportId, airlineId, terminal, startDate, endDate, isActive);
    }

    public void Update(int airportId, int airlineId, Terminal terminal, DateOnly startDate, DateOnly? endDate, bool isActive)
    {
        AirportId = airportId;
        AirlineId = airlineId;
        Terminal = terminal;
        StartDate = startDate;
        EndDate = endDate;
        IsActive = isActive;
    }
}