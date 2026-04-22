using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Entity;

[Table("BaggageTypes")]
public class BaggageTypeEntity
{
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    public string Name { get; set; } = null!;

    [Column("MaxWeightKg")]
    public decimal MaxWeightKg { get; set; }

    [Column("BasePrice")]
    public decimal BasePrice { get; set; }

    public ICollection<BaggageEntity> Baggages { get; set; } = new List<BaggageEntity>();
}
