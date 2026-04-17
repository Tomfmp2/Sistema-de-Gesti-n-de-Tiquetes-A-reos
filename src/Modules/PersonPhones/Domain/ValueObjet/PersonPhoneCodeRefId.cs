namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.ValueObjet;

public sealed class PersonPhoneCodeRefId
{
    public int Value { get; }

    public PersonPhoneCodeRefId(int value) => Value = value;

    public static PersonPhoneCodeRefId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PersonPhoneCodeRefId(value);
    }
}
