namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.ValueObjet;

public sealed class PersonPhonePersonId
{
    public int Value { get; }

    public PersonPhonePersonId(int value) => Value = value;

    public static PersonPhonePersonId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PersonPhonePersonId(value);
    }
}
