using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;

[Table("airports")]
public class AirportEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string IataCode { get; set; } = null!;
    public string? IcaoCode { get; set; }
    public int CityId { get; set; }
}