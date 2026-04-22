using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Infrastructure.Entity;

public class PersonPhoneEntity
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int PhoneCodeId { get; set; }
    public string? Number { get; set; }
    public bool IsPrimary { get; set; }

    public PersonEntity? Person { get; set; }
    public PhoneCodeEntity? PhoneCode { get; set; }
}
