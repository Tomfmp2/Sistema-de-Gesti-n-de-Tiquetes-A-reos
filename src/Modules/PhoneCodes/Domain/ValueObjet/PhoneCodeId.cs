namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.ValueObjet;

public sealed class PhoneCodeId
{
    public int Value { get; }

    public PhoneCodeId(int value) => Value = value;

    public static PhoneCodeId Unpersisted => new(0);

    public static PhoneCodeId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PhoneCodeId(value);
    }
}
