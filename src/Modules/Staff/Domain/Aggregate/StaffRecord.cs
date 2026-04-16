using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.Aggregate;

public sealed class StaffRecord
{
    public StaffId Id { get; private set; }
    public PersonId PersonId { get; private set; }
    public PositionId PositionId { get; private set; }
    public AirlineId? AirlineId { get; private set; }
    public AirportId? AirportId { get; private set; }
    public HireDate HireDate { get; private set; }
    public IsActive IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private StaffRecord(
        StaffId id,
        PersonId personId,
        PositionId positionId,
        AirlineId? airlineId,
        AirportId? airportId,
        HireDate hireDate,
        IsActive isActive,
        DateTime createdAt,
        DateTime updatedAt)
    {
        Id = id;
        PersonId = personId;
        PositionId = positionId;
        AirlineId = airlineId;
        AirportId = airportId;
        HireDate = hireDate;
        IsActive = isActive;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static StaffRecord Create(
        StaffId id,
        PersonId personId,
        PositionId positionId,
        AirlineId? airlineId,
        AirportId? airportId,
        HireDate hireDate,
        IsActive isActive)
    {
        var now = DateTime.Now;
        return new StaffRecord(id, personId, positionId, airlineId, airportId, hireDate, isActive, now, now);
    }

    public static StaffRecord Reconstitute(
        StaffId id,
        PersonId personId,
        PositionId positionId,
        AirlineId? airlineId,
        AirportId? airportId,
        HireDate hireDate,
        IsActive isActive,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new StaffRecord(id, personId, positionId, airlineId, airportId, hireDate, isActive, createdAt, updatedAt);
    }

    public void UpdatePosition(PositionId positionId)
    {
        PositionId = positionId;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateAirline(AirlineId? airlineId)
    {
        AirlineId = airlineId;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateAirport(AirportId? airportId)
    {
        AirportId = airportId;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateHireDate(HireDate hireDate)
    {
        HireDate = hireDate;
        UpdatedAt = DateTime.Now;
    }

    public void UpdateIsActive(IsActive isActive)
    {
        IsActive = isActive;
        UpdatedAt = DateTime.Now;
    }
}