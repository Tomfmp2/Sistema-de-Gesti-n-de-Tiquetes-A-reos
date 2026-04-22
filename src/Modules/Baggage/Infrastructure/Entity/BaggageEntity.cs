using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Entity;

public class BaggageEntity
{
    public int Id { get; set; }
    public int CheckinId { get; set; }
    public int BaggageTypeId { get; set; }
    public decimal WeightKg { get; set; }
    public decimal ChargedPrice { get; set; }

    public CheckinEntity? Checkin { get; set; }
    public BaggageTypeEntity? BaggageType { get; set; }
}
