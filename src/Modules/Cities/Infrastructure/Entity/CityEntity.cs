using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;

public class CityEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int RegionId { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public RegionEntity? Region { get; set; }
    public ICollection<AirportEntity> Airports { get; set; } = new List<AirportEntity>();
    public ICollection<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();
}
