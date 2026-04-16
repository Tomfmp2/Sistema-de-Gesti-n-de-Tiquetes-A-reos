namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.ValueObjet;

public sealed class PersonLastName
{
    public string Value { get; }

    public PersonLastName(string value) => Value = value;

    public static PersonLastName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El apellido no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 100)
        {
            throw new ArgumentException("Máximo 100 caracteres.");
        }

        return new PersonLastName(t);
    }
}
