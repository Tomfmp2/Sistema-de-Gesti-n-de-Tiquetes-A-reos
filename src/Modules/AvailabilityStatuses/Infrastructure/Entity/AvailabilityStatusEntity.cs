using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Entity;

[Table("availability_statuses")]
public class AvailabilityStatusEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}