using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Aggregate;

public sealed class PassengerType
{
    public PassengerTypeId Id { get; private set; }
    public PassengerTypeName Name { get; private set; }
    public int? MinAge { get; private set; }
    public int? MaxAge { get; private set; }

    private PassengerType(
        PassengerTypeId id,
        PassengerTypeName name,
        int? minAge,
        int? maxAge
    )
    {
        Id = id;
        Name = name;
        MinAge = minAge;
        MaxAge = maxAge;
    }

    public static PassengerType CreateNew(PassengerTypeName name, int? minAge, int? maxAge)
    {
        return new PassengerType(PassengerTypeId.Unpersisted, name, minAge, maxAge);
    }

    public static PassengerType Create(
        PassengerTypeId id,
        PassengerTypeName name,
        int? minAge,
        int? maxAge
    )
    {
        return new PassengerType(id, name, minAge, maxAge);
    }
}
