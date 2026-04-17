namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;

public class CountryEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CodeIso { get; set; }
    public int ContinentId { get; set; }
}
