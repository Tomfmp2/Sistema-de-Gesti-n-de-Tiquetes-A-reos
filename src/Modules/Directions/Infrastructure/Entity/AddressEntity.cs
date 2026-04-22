using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;

/// <summary>
/// Physical address (maps to table <c>addresses</c>).
/// </summary>
public class AddressEntity
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public int StreetTypeId { get; set; }
    public string? StreetName { get; set; }
    public string? StreetNumber { get; set; }
    public string? Complement { get; set; }
    public string? PostalCode { get; set; }

    public CityEntity? City { get; set; }
    public StreetTypeEntity? StreetType { get; set; }
    public ICollection<PersonEntity> Persons { get; set; } = new List<PersonEntity>();
}
