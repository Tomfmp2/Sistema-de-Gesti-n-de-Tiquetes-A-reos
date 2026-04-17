namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;

public class DirectionEntity
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public int StreetTypeId { get; set; }
    public string? StreetName { get; set; }
    public string? StreetNumber { get; set; }
    public string? Complement { get; set; }
    public string? PostalCode { get; set; }
}
