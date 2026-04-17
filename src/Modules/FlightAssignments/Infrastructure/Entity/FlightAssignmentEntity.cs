using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.Entity;

public sealed class FlightAssignmentEntity
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public int StaffId { get; set; }
    public int FlightRoleId { get; set; }

    // Navigation properties
    public FlightEntity? Flight { get; set; }
    public StaffEntity? Staff { get; set; }
    // TODO: FlightRoleEntity - módulo FlightRoles no existe aún
}
