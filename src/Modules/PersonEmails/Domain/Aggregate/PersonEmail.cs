using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Aggregate;

public sealed class PersonEmail
{
    public PersonEmailId Id { get; private set; }
    public PersonEmailPersonId PersonId { get; private set; }
    public PersonEmailLocalPart EmailLocalPart { get; private set; }
    public PersonEmailDomainRefId EmailDomainId { get; private set; }
    public bool IsPrimary { get; private set; }

    private PersonEmail(
        PersonEmailId id,
        PersonEmailPersonId personId,
        PersonEmailLocalPart emailLocalPart,
        PersonEmailDomainRefId emailDomainId,
        bool isPrimary
    )
    {
        Id = id;
        PersonId = personId;
        EmailLocalPart = emailLocalPart;
        EmailDomainId = emailDomainId;
        IsPrimary = isPrimary;
    }

    public static PersonEmail CreateNew(
        PersonEmailPersonId personId,
        PersonEmailLocalPart emailLocalPart,
        PersonEmailDomainRefId emailDomainId,
        bool isPrimary
    )
    {
        return new PersonEmail(
            PersonEmailId.Unpersisted,
            personId,
            emailLocalPart,
            emailDomainId,
            isPrimary
        );
    }

    public static PersonEmail Create(
        PersonEmailId id,
        PersonEmailPersonId personId,
        PersonEmailLocalPart emailLocalPart,
        PersonEmailDomainRefId emailDomainId,
        bool isPrimary
    )
    {
        return new PersonEmail(id, personId, emailLocalPart, emailDomainId, isPrimary);
    }
}
