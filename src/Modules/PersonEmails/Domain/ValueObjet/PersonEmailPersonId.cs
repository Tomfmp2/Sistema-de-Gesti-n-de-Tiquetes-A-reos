namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.ValueObjet;

public sealed class PersonEmailPersonId
{
    public int Value { get; }

    public PersonEmailPersonId(int value) => Value = value;

    public static PersonEmailPersonId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PersonEmailPersonId(value);
    }
}
