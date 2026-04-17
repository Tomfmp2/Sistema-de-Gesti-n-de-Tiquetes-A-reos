using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Aggregate;

public sealed class PersonPhone
{
    public PersonPhoneId Id { get; private set; }
    public PersonPhonePersonId PersonId { get; private set; }
    public PersonPhoneCodeRefId PhoneCodeId { get; private set; }
    public PersonPhoneLineNumber Number { get; private set; }
    public bool IsPrimary { get; private set; }

    private PersonPhone(
        PersonPhoneId id,
        PersonPhonePersonId personId,
        PersonPhoneCodeRefId phoneCodeId,
        PersonPhoneLineNumber number,
        bool isPrimary
    )
    {
        Id = id;
        PersonId = personId;
        PhoneCodeId = phoneCodeId;
        Number = number;
        IsPrimary = isPrimary;
    }

    public static PersonPhone CreateNew(
        PersonPhonePersonId personId,
        PersonPhoneCodeRefId phoneCodeId,
        PersonPhoneLineNumber number,
        bool isPrimary
    )
    {
        return new PersonPhone(
            PersonPhoneId.Unpersisted,
            personId,
            phoneCodeId,
            number,
            isPrimary
        );
    }

    public static PersonPhone Create(
        PersonPhoneId id,
        PersonPhonePersonId personId,
        PersonPhoneCodeRefId phoneCodeId,
        PersonPhoneLineNumber number,
        bool isPrimary
    )
    {
        return new PersonPhone(id, personId, phoneCodeId, number, isPrimary);
    }
}
