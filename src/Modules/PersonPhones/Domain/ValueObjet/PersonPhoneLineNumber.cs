namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.ValueObjet;

public sealed class PersonPhoneLineNumber
{
    public string Value { get; }

    public PersonPhoneLineNumber(string value) => Value = value;

    public static PersonPhoneLineNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El número no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 20)
        {
            throw new ArgumentException("Máximo 20 caracteres.");
        }

        return new PersonPhoneLineNumber(t);
    }
}
