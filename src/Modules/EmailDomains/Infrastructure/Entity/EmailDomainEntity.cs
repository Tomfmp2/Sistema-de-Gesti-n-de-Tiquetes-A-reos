using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Entity;

public class EmailDomainEntity
{
    public int Id { get; set; }
    public string? Domain { get; set; }

    public ICollection<PersonEmailEntity> PersonEmails { get; set; } = new List<PersonEmailEntity>();
}
