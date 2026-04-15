namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Entity;

public class RegionEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int CountryId { get; set; }
}
