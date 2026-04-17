using System;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Entity;

public class ContinentEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }

    // Navigation properties
    public ICollection<CountryEntity> Countries { get; set; } = new List<CountryEntity>();
}
