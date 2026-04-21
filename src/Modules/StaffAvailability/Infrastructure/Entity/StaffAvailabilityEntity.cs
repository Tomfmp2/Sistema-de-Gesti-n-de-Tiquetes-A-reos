using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.Entity;

[Table("StaffAvailability")]
public class StaffAvailabilityEntity
{
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }

    [Column("StaffId")]
    public int StaffId { get; set; }

    [Column("AvailabilityStatusId")]
    public int AvailabilityStatusId { get; set; }

    [Column("StartDate")]
    public DateTime StartDate { get; set; }

    [Column("EndDate")]
    public DateTime EndDate { get; set; }

    [Column("Observation")]
    public string? Observation { get; set; }
}