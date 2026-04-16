namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.ValueObjet;

public sealed class PersonPhoneId
{
    public int Value { get; }

    public PersonPhoneId(int value) => Value = value;

    public static PersonPhoneId Unpersisted => new(0);

    public static PersonPhoneId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PersonPhoneId(value);
    }
}
