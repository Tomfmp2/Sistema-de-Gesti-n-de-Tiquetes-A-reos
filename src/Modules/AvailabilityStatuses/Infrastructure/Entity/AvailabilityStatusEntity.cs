using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Entity;

[Table("availability_statuses")]
public class AvailabilityStatusEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<StaffAvailabilityEntity> StaffAvailabilities { get; set; } =
        new List<StaffAvailabilityEntity>();
}