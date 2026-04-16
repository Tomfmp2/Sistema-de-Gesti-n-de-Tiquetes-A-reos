using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

[Table("staff")]
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
}