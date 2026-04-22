using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;

public class PassengerTypeEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }

    public ICollection<PassengerEntity> Passengers { get; set; } = new List<PassengerEntity>();
    public ICollection<FareEntity> Fares { get; set; } = new List<FareEntity>();
}
