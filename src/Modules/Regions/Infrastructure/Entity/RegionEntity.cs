using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Entity;

public class RegionEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int CountryId { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public CountryEntity? Country { get; set; }
    public ICollection<CityEntity> Cities { get; set; } = new List<CityEntity>();
}
