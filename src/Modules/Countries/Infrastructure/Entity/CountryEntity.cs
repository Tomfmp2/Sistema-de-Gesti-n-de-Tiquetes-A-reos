using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;

public class CountryEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CodeIso { get; set; }
    public int ContinentId { get; set; }

    // Navigation properties
    public ContinentEntity? Continent { get; set; }
    public ICollection<RegionEntity> Regions { get; set; } = new List<RegionEntity>();
}
