using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Entity;

[Table("aircraft_manufacturers")]
public class AircraftManufacturerEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("country")]
    public string Country { get; set; }

    public ICollection<AircraftModelEntity> Models { get; set; } = new List<AircraftModelEntity>();
}