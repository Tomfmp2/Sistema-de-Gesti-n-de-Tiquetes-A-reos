using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Aggregate;

public class CabinType
{
    public CabinTypeId Id { get; internal set; }
    public CabinTypeName Name { get; private set; }

    private CabinType(CabinTypeId id, CabinTypeName name)
    {
        Id = id;
        Name = name;
    }

    public static CabinType Create(CabinTypeName name)
    {
        return new CabinType(
            CabinTypeId.Create(0), // will be set by DB
            name);
    }

    public static CabinType Reconstitute(CabinTypeId id, CabinTypeName name)
    {
        return new CabinType(id, name);
    }
}