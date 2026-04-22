using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Entity;

[Table("AircraftManufacturers")]
public class AircraftManufacturerEntity
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    public string Name { get; set; }

    [Column("Country")]
    public string Country { get; set; }

    public ICollection<AircraftModelEntity> Models { get; set; } = new List<AircraftModelEntity>();
}