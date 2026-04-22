using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Entity;

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

    public RouteEntity? Route { get; set; }
    public CabinTypeEntity? CabinType { get; set; }
    public PassengerTypeEntity? PassengerType { get; set; }
    public SeasonEntity? Season { get; set; }
}
