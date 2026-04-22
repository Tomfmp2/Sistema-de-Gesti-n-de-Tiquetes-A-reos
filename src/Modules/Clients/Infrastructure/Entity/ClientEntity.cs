using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;

public class ClientEntity
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public DateTime CreatedAt { get; set; }

    public PersonEntity? Person { get; set; }
    public ICollection<ReservationEntity> Reservations { get; set; } = new List<ReservationEntity>();
}
