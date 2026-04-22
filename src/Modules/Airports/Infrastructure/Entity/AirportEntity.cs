using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

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
    public ICollection<RouteEntity> OriginRoutes { get; set; } = new List<RouteEntity>();
    public ICollection<RouteEntity> DestinationRoutes { get; set; } = new List<RouteEntity>();
    public ICollection<RouteLayoverEntity> RouteLayovers { get; set; } = new List<RouteLayoverEntity>();
    public ICollection<StaffEntity> Staff { get; set; } = new List<StaffEntity>();
}