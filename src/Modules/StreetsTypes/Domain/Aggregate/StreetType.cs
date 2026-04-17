using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Aggregate;

public sealed class StreetType
{
    public StreetTypeId Id { get; private set; }
    public StreetTypeName Name { get; private set; }

    private StreetType(StreetTypeId id, StreetTypeName name)
    {
        Id = id;
        Name = name;
    }

    public static StreetType CreateNew(StreetTypeName name)
    {
        return new StreetType(StreetTypeId.Unpersisted, name);
    }

    public static StreetType Create(StreetTypeId id, StreetTypeName name)
    {
        return new StreetType(id, name);
    }
}
