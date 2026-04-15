namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;

public class CountryEntity
{
    public int Id { get; set; }
    public string? name { get; set; }
    public string? codeIso { get; set; }
    public int continentId { get; set; }
}
