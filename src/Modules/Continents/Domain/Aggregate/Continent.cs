namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Aggregate;

public class Continent
{
    public string Id { get; private set; }
    public string Name { get; private set; }

    private Continent()
    {
        Id = string.Empty;
        Name = string.Empty;
    }

    public Continent(string id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Continent Create(string id, string name)
    {
        return new Continent(id, name);
    }
}
