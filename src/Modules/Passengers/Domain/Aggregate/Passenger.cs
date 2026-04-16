using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Aggregate;

public sealed class Passenger
{
    public PassengerId Id { get; private set; }
    public PassengerPersonId PersonId { get; private set; }
    public PassengerTypeRefId PassengerTypeId { get; private set; }

    private Passenger(PassengerId id, PassengerPersonId personId, PassengerTypeRefId passengerTypeId)
    {
        Id = id;
        PersonId = personId;
        PassengerTypeId = passengerTypeId;
    }

    public static Passenger CreateNew(PassengerPersonId personId, PassengerTypeRefId passengerTypeId)
    {
        return new Passenger(PassengerId.Unpersisted, personId, passengerTypeId);
    }

    public static Passenger Create(
        PassengerId id,
        PassengerPersonId personId,
        PassengerTypeRefId passengerTypeId
    )
    {
        return new Passenger(id, personId, passengerTypeId);
    }
}
