namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.ValueObjet;

public sealed class PersonEmailLocalPart
{
    public string Value { get; }

    public PersonEmailLocalPart(string value) => Value = value;

    public static PersonEmailLocalPart Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("La parte local del correo no puede estar vacía.");
        }

        var t = value.Trim();
        if (t.Length > 100)
        {
            throw new ArgumentException("Máximo 100 caracteres.");
        }

        return new PersonEmailLocalPart(t);
    }
}
