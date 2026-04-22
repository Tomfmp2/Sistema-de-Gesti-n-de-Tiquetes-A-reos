using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Infrastructure.Entity;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;

public class PersonEntity
{
    public int Id { get; set; }
    public int DocumentTypeId { get; set; }
    public string? DocumentNumber { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public char? Gender { get; set; }
    public int? AddressId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public DocumentTypeEntity? DocumentType { get; set; }
    public AddressEntity? Address { get; set; }
    public UserEntity? User { get; set; }
    public ClientEntity? Client { get; set; }
    public PassengerEntity? Passenger { get; set; }
    public ICollection<PersonEmailEntity> Emails { get; set; } = new List<PersonEmailEntity>();
    public ICollection<PersonPhoneEntity> Phones { get; set; } = new List<PersonPhoneEntity>();
    public StaffEntity? Staff { get; set; }
}
