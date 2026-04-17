using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;

[Table("aircraft_models")]
public class AircraftModelEntity
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("manufacturer_id")]
    public int ManufacturerId { get; set; }

    [Column("model_name")]
    public string ModelName { get; set; }

    [Column("max_capacity")]
    public int MaxCapacity { get; set; }

    [Column("max_takeoff_weight_kg")]
    public decimal? MaxTakeoffWeightKg { get; set; }

    [Column("fuel_consumption_kg_h")]
    public decimal? FuelConsumptionKgH { get; set; }

    [Column("cruising_speed_kmh")]
    public int? CruisingSpeedKmh { get; set; }

    [Column("cruising_altitude_ft")]
    public int? CruisingAltitudeFt { get; set; }

    // Navigation properties
    public AircraftManufacturerEntity? Manufacturer { get; set; }
    public ICollection<AircraftEntity> Aircrafts { get; set; } = new List<AircraftEntity>();
}