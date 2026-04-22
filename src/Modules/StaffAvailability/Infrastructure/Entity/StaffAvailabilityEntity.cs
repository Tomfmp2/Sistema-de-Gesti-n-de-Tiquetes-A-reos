using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Infrastructure.Entity;

public class StaffAvailabilityEntity
{
    public int Id { get; set; }
    public int StaffId { get; set; }
    public int AvailabilityStatusId { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public string? Notes { get; set; }

    public StaffEntity? Staff { get; set; }
    public AvailabilityStatusEntity? AvailabilityStatus { get; set; }
}
