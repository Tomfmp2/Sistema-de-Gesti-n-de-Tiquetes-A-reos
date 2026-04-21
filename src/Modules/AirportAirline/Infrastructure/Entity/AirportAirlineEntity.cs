using System.ComponentModel.DataAnnotations.Schema;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.Entity;

[Table("AirportAirline")]
public class AirportAirlineEntity
{
    public int Id { get; set; }
    public int AirportId { get; set; }
    public int AirlineId { get; set; }
    public string? Terminal { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public AirportEntity? Airport { get; set; }
    public AirlineEntity? Airline { get; set; }
}