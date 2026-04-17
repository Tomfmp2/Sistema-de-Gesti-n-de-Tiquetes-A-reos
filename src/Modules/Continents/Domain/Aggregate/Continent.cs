using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Aggregate;

public sealed class Continent
{
    public ContinentId Id { get; private set; }
    public ContinentName Name { get; private set; }

    private Continent(ContinentId id, ContinentName name)
    {
        Id = id;
        Name = name;
    }

    public static Continent CreateNew(ContinentName name)
    {
        return new Continent(ContinentId.Unpersisted, name);
    }

    public static Continent Create(ContinentId id, ContinentName name)
    {
        return new Continent(id, name);
    }
}
