using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Aggregate
{
    public sealed record Region
    {
        public RegionId Id { get; private set; }
        public RegionName Name { get; private set; }
        public RegionType Type { get; private set; }
        public RegionCuntryId CountryId { get; private set; }

        private Region(RegionId id, RegionName name, RegionType type, RegionCuntryId countryId)
        {
            Id = id;
            Name = name;
            Type = type;
            CountryId = countryId;
        }

        public static Region Create(
            RegionId id,
            RegionName name,
            RegionType type,
            RegionCuntryId countryId
        )
        {
            return new Region(id, name, type, countryId);
        }
    }
}
