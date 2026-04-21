using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Entity;

[Table("Baggage")]
public class BaggageEntity
{
    public int Id { get; set; }
    public int CheckinId { get; set; }
    public int BaggageTypeId { get; set; }
    public decimal WeightKg { get; set; }
    public decimal ChargedPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
