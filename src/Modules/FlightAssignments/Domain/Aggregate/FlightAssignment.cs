using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Domain.Aggregate;

public sealed class FlightAssignment
{
    public FlightAssignmentId Id { get; private set; }
    public FlightId FlightId { get; private set; }
    public StaffId StaffId { get; private set; }
    public FlightRoleId FlightRoleId { get; private set; }

    private FlightAssignment(
        FlightAssignmentId id,
        FlightId flightId,
        StaffId staffId,
        FlightRoleId flightRoleId)
    {
        Id = id;
        FlightId = flightId;
        StaffId = staffId;
        FlightRoleId = flightRoleId;
    }

    public static FlightAssignment Create(
        FlightId flightId,
        StaffId staffId,
        FlightRoleId flightRoleId)
    {
        return new FlightAssignment(
            new FlightAssignmentId(0),
            flightId,
            staffId,
            flightRoleId);
    }

    public static FlightAssignment Reconstitute(
        FlightAssignmentId id,
        FlightId flightId,
        StaffId staffId,
        FlightRoleId flightRoleId)
    {
        return new FlightAssignment(id, flightId, staffId, flightRoleId);
    }

    public void UpdateFlightId(FlightId flightId)
    {
        FlightId = flightId ?? throw new ArgumentNullException(nameof(flightId));
    }

    public void UpdateStaffId(StaffId staffId)
    {
        StaffId = staffId ?? throw new ArgumentNullException(nameof(staffId));
    }

    public void UpdateFlightRoleId(FlightRoleId flightRoleId)
    {
        FlightRoleId = flightRoleId ?? throw new ArgumentNullException(nameof(flightRoleId));
    }
}
