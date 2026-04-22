using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Aggregate;

public sealed class Person
{
    public PersonId Id { get; private set; }
    public PersonDocumentTypeRefId DocumentTypeId { get; private set; }
    public PersonDocumentNumber DocumentNumber { get; private set; }
    public PersonFirstName FirstName { get; private set; }
    public PersonLastName LastName { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public char? Gender { get; private set; }
    public int? AddressId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Person(
        PersonId id,
        PersonDocumentTypeRefId documentTypeId,
        PersonDocumentNumber documentNumber,
        PersonFirstName firstName,
        PersonLastName lastName,
        DateTime? birthDate,
        char? gender,
        int? addressId,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        Id = id;
        DocumentTypeId = documentTypeId;
        DocumentNumber = documentNumber;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Gender = gender;
        AddressId = addressId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Person CreateNew(
        PersonDocumentTypeRefId documentTypeId,
        PersonDocumentNumber documentNumber,
        PersonFirstName firstName,
        PersonLastName lastName,
        DateTime? birthDate,
        char? gender,
        int? addressId
    )
    {
        return new Person(
            PersonId.Unpersisted,
            documentTypeId,
            documentNumber,
            firstName,
            lastName,
            birthDate,
            NormalizeGender(gender),
            addressId,
            default,
            default
        );
    }

    public static Person Create(
        PersonId id,
        PersonDocumentTypeRefId documentTypeId,
        PersonDocumentNumber documentNumber,
        PersonFirstName firstName,
        PersonLastName lastName,
        DateTime? birthDate,
        char? gender,
        int? addressId,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new Person(
            id,
            documentTypeId,
            documentNumber,
            firstName,
            lastName,
            birthDate,
            NormalizeGender(gender),
            addressId,
            createdAt,
            updatedAt
        );
    }

    private static char? NormalizeGender(char? gender)
    {
        if (gender is null)
        {
            return null;
        }

        var c = gender.Value;
        if (char.IsLetter(c))
        {
            return char.ToUpperInvariant(c);
        }

        throw new ArgumentException("Género debe ser una letra o null.");
    }
}
