using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.Entity;

[Table("staff_availability")]
public class StaffAvailabilityEntity
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("staff_id")]
    public Guid StaffId { get; set; }

    [Column("availability_status_id")]
    public Guid AvailabilityStatusId { get; set; }

    [Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("end_date")]
    public DateTime EndDate { get; set; }

    [Column("observation")]
    public string? Observation { get; set; }
}