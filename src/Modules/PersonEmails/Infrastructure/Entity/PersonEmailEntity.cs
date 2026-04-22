using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Infrastructure.Entity;

public class PersonEmailEntity
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public string? EmailLocalPart { get; set; }
    public int EmailDomainId { get; set; }
    public bool IsPrimary { get; set; }

    public PersonEntity? Person { get; set; }
    public EmailDomainEntity? EmailDomain { get; set; }
}
