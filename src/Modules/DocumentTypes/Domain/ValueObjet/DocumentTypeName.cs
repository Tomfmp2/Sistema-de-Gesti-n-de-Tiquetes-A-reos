namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.ValueObjet;

public sealed class DocumentTypeName
{
    public string Value { get; }

    public DocumentTypeName(string value) => Value = value;

    public static DocumentTypeName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El nombre no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 50)
        {
            throw new ArgumentException("Máximo 50 caracteres.");
        }

        return new DocumentTypeName(t);
    }
}
