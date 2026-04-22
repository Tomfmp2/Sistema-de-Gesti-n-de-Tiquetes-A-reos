using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;

public class DocumentTypeEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }

    public ICollection<PersonEntity> Persons { get; set; } = new List<PersonEntity>();
}
