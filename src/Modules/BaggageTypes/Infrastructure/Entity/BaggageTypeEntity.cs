namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Entity;

using System.ComponentModel.DataAnnotations.Schema;

[Table("baggage_types")]
public class BaggageTypeEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = null!;

    [Column("max_weight_kg")]
    public decimal MaxWeightKg { get; set; }

    [Column("base_price")]
    public decimal BasePrice { get; set; }
}
