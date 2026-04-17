using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;

[Table("airlines")]
public class AirlineEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string IataCode { get; set; } = null!;
    public int OriginCountryId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation properties
    public CountryEntity? OriginCountry { get; set; }
    public ICollection<AirportAirlineEntity> AirportAirlines { get; set; } = new List<AirportAirlineEntity>();
}