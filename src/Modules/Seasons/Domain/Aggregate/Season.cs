using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.Aggregate;

public sealed class Season
{
    public SeasonId Id { get; }
    public SeasonName Name { get; }
    public SeasonDescription Description { get; }
    public PriceFactor PriceFactor { get; }

    private Season(SeasonId id, SeasonName name, SeasonDescription description, PriceFactor priceFactor)
    {
        Id = id;
        Name = name;
        Description = description;
        PriceFactor = priceFactor;
    }

    public static Season Create(SeasonName name, SeasonDescription description, PriceFactor priceFactor)
    {
        return new Season(SeasonId.Reconstitute(0), name, description, priceFactor);
    }

    public static Season Reconstitute(SeasonId id, SeasonName name, SeasonDescription description, PriceFactor priceFactor)
    {
        return new Season(id, name, description, priceFactor);
    }
}