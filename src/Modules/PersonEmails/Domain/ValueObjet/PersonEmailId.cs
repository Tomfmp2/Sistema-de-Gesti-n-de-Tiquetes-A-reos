namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.ValueObjet;

public sealed class PersonEmailId
{
    public int Value { get; }

    public PersonEmailId(int value) => Value = value;

    public static PersonEmailId Unpersisted => new(0);

    public static PersonEmailId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PersonEmailId(value);
    }
}
