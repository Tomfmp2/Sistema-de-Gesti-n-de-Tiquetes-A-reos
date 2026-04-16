namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.ValueObjet;

public sealed class PersonEmailDomainRefId
{
    public int Value { get; }

    public PersonEmailDomainRefId(int value) => Value = value;

    public static PersonEmailDomainRefId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PersonEmailDomainRefId(value);
    }
}
