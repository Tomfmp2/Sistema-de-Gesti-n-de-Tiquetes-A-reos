namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.ValueObjet;

public sealed class PersonId
{
    public int Value { get; }

    public PersonId(int value) => Value = value;

    public static PersonId Unpersisted => new(0);

    public static PersonId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PersonId(value);
    }
}
