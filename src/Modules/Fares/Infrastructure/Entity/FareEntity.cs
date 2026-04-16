namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Entity;

public class FareEntity
{
    public int Id { get; set; }
    public int RouteId { get; set; }
    public int CabinTypeId { get; set; }
    public int PassengerTypeId { get; set; }
    public int SeasonId { get; set; }
    public decimal BasePrice { get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
}
