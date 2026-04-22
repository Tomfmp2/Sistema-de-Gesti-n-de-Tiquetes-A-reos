using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;

[Table("AircraftModels")]
public class AircraftModelEntity
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("ManufacturerId")]
    public int ManufacturerId { get; set; }

    [Column("ModelName")]
    public string ModelName { get; set; } = null!;

    [Column("MaxCapacity")]
    public int MaxCapacity { get; set; }

    [Column("MaxTakeoffWeightKg")]
    public decimal? MaxTakeoffWeightKg { get; set; }

    [Column("FuelConsumptionKgH")]
    public decimal? FuelConsumptionKgH { get; set; }

    [Column("CruisingSpeedKmh")]
    public int? CruisingSpeedKmh { get; set; }

    [Column("CruisingAltitudeFt")]
    public int? CruisingAltitudeFt { get; set; }

    // Navigation properties
    public AircraftManufacturerEntity? Manufacturer { get; set; }
    public ICollection<AircraftEntity> Aircrafts { get; set; } = new List<AircraftEntity>();
}