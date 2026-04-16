using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Aggregate;

public class StaffAvailabilityRecord
{
    public StaffAvailabilityId Id { get; private set; }
    public StaffId StaffId { get; private set; }
    public AvailabilityStatusId AvailabilityStatusId { get; private set; }
    public StartDate StartDate { get; private set; }
    public EndDate EndDate { get; private set; }
    public Observation? Observation { get; private set; }

    private StaffAvailabilityRecord(
        StaffAvailabilityId id,
        StaffId staffId,
        AvailabilityStatusId availabilityStatusId,
        StartDate startDate,
        EndDate endDate,
        Observation? observation)
    {
        Id = id;
        StaffId = staffId;
        AvailabilityStatusId = availabilityStatusId;
        StartDate = startDate;
        EndDate = endDate;
        Observation = observation;
    }

    public static StaffAvailabilityRecord Create(
        StaffAvailabilityId id,
        StaffId staffId,
        AvailabilityStatusId availabilityStatusId,
        StartDate startDate,
        EndDate endDate,
        Observation? observation)
    {
        return new StaffAvailabilityRecord(id, staffId, availabilityStatusId, startDate, endDate, observation);
    }

    public static StaffAvailabilityRecord Reconstitute(
        StaffAvailabilityId id,
        StaffId staffId,
        AvailabilityStatusId availabilityStatusId,
        StartDate startDate,
        EndDate endDate,
        Observation? observation)
    {
        return new StaffAvailabilityRecord(id, staffId, availabilityStatusId, startDate, endDate, observation);
    }
}