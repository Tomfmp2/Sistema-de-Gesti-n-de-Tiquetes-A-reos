namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.Entity;

public sealed class FlightAssignmentEntity
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public int StaffId { get; set; }
    public int FlightRoleId { get; set; }
}
