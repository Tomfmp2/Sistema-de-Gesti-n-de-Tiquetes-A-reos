namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.ValueObjet;

public sealed class PersonDocumentTypeRefId
{
    public int Value { get; }

    public PersonDocumentTypeRefId(int value) => Value = value;

    public static PersonDocumentTypeRefId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PersonDocumentTypeRefId(value);
    }
}
