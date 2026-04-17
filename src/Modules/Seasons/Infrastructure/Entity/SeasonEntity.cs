using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Entity;

public class SeasonEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal PriceFactor { get; set; }

    public static SeasonEntity FromDomain(Season season)
    {
        return new SeasonEntity
        {
            Id = season.Id?.Value ?? 0,
            Name = season.Name.Value,
            Description = season.Description.Value,
            PriceFactor = season.PriceFactor.Value
        };
    }

    public Season ToDomain()
    {
        return Season.Reconstitute(
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject.SeasonId.Reconstitute(Id),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject.SeasonName.Reconstitute(Name),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject.SeasonDescription.Reconstitute(Description),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject.PriceFactor.Reconstitute(PriceFactor)
        );
    }
}