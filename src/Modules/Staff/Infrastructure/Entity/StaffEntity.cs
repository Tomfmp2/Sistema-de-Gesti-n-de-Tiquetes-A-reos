using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

[Table("Staff")]
public class StaffEntity
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int PositionId { get; set; }
    public int? AirlineId { get; set; }
    public int? AirportId { get; set; }
    public DateOnly HireDate { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public PersonEntity? Person { get; set; }
    public StaffPositionEntity? Position { get; set; }
    public AirlineEntity? Airline { get; set; }
    public AirportEntity? Airport { get; set; }
    public ICollection<FlightAssignmentEntity> FlightAssignments { get; set; } = new List<FlightAssignmentEntity>();
}