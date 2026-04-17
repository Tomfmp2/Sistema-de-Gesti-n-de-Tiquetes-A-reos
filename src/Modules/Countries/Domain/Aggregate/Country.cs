using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Aggregate;

public sealed class Country
{
    public CountryId Id { get; private set; }
    public CountryName Name { get; private set; }
    public CountryCodigoIso CodeIso { get; private set; }
    public CountryContinentId ContinentId { get; private set; }

    private Country(
        CountryId id,
        CountryName name,
        CountryCodigoIso codeIso,
        CountryContinentId continentId
    )
    {
        Id = id;
        Name = name;
        CodeIso = codeIso;
        ContinentId = continentId;
    }

    public static Country CreateNew(
        CountryName name,
        CountryCodigoIso codeIso,
        CountryContinentId continentId
    )
    {
        return new Country(CountryId.Unpersisted, name, codeIso, continentId);
    }

    public static Country Create(
        CountryId id,
        CountryName name,
        CountryCodigoIso codeIso,
        CountryContinentId continentId
    )
    {
        return new Country(id, name, codeIso, continentId);
    }
}
