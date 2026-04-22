using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Infrastructure.Entity;

public class CheckinStatusEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<CheckinEntity> Checkins { get; set; } = new List<CheckinEntity>();
}
