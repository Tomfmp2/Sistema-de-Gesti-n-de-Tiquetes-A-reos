using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Aggregate;

public class Continent
{
    public ContinentsId Id { get; private set; }
    public ContinentName Name { get; private set; }

    private Continent(ContinentsId id, ContinentName name)
    {
        Id = id;
        Name = name;
    }

    public static Continent Create(ContinentsId id, ContinentName name)
    {
        return new Continent(id, name);
    }
}
