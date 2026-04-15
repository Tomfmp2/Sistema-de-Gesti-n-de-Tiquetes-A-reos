namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

public class PersonEmailEntity
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int? EmailDomainId { get; set; }
    public string? Email { get; set; }
}
