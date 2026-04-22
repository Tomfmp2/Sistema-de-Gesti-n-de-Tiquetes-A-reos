using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Entity;

public class PhoneCodeEntity
{
    public int Id { get; set; }
    public string? CountryDialCode { get; set; }
    public string? CountryName { get; set; }

    public ICollection<PersonPhoneEntity> PersonPhones { get; set; } = new List<PersonPhoneEntity>();
}
