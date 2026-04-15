using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Aggregate;

public class Country
{
    public CountryId Id { get; private set; }
    public CountryName Name { get; private set; }
    public CountryContinentId ContinentId { get; private set; }

    private Country(CountryId id, CountryName name, CountryContinentId continentId)
    {
        Id = id;
        Name = name;
        ContinentId = continentId;
    }

    public static Country Create(CountryId id, CountryName name, CountryContinentId continentId)
    {
        return new Country(id, name, continentId);
    }
}
