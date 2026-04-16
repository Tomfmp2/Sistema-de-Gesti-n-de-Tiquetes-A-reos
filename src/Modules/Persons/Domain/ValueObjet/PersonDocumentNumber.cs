namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.ValueObjet;

public sealed class PersonDocumentNumber
{
    public string Value { get; }

    public PersonDocumentNumber(string value) => Value = value;

    public static PersonDocumentNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El documento no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 30)
        {
            throw new ArgumentException("Máximo 30 caracteres.");
        }

        return new PersonDocumentNumber(t);
    }
}
