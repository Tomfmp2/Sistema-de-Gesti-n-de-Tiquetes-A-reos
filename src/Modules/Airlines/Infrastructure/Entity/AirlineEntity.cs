using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;

[Table("Airlines")]
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
    public ICollection<FlightEntity> Flights { get; set; } = new List<FlightEntity>();
    public ICollection<AircraftEntity> Aircraft { get; set; } = new List<AircraftEntity>();
    public ICollection<StaffEntity> Staff { get; set; } = new List<StaffEntity>();
}