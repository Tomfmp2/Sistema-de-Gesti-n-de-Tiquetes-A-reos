namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.ValueObjet;

public sealed class PersonFirstName
{
    public string Value { get; }

    public PersonFirstName(string value) => Value = value;

    public static PersonFirstName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El nombre no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 100)
        {
            throw new ArgumentException("Máximo 100 caracteres.");
        }

        return new PersonFirstName(t);
    }
}
