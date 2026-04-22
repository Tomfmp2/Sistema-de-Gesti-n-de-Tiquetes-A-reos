using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;

public class StreetTypeEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();
}
