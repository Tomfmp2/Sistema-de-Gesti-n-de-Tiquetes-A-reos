namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Entity;

using System.ComponentModel.DataAnnotations.Schema;

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
}
