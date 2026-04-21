using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;

[Table("Airports")]
public class AirportEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string IataCode { get; set; } = null!;
    public string? IcaoCode { get; set; }
    public int CityId { get; set; }

    // Navigation properties
    public CityEntity? City { get; set; }
    public ICollection<AirportAirlineEntity> AirportAirlines { get; set; } = new List<AirportAirlineEntity>();
}