using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Aggregate;

public sealed class City
{
    public CityId Id { get; private set; }
    public CityName Name { get; private set; }
    public CityRegionId RegionId { get; private set; }

    private City(CityId id, CityName name, CityRegionId regionId)
    {
        Id = id;
        Name = name;
        RegionId = regionId;
    }

    public static City CreateNew(CityName name, CityRegionId regionId)
    {
        return new City(CityId.Unpersisted, name, regionId);
    }

    public static City Create(CityId id, CityName name, CityRegionId regionId)
    {
        return new City(id, name, regionId);
    }
}
