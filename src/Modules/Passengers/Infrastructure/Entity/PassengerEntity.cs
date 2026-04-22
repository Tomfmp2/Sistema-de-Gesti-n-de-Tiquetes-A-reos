using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Infrastructure.Entity;

public class PassengerEntity
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int PassengerTypeId { get; set; }

    public PersonEntity? Person { get; set; }
    public PassengerTypeEntity? PassengerType { get; set; }
    public ICollection<ReservationPassengerEntity> ReservationPassengers { get; set; } =
        new List<ReservationPassengerEntity>();
}
