namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.ValueObjet;

public sealed class DocumentTypeCode
{
    public string Value { get; }

    public DocumentTypeCode(string value) => Value = value;

    public static DocumentTypeCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El código no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 10)
        {
            throw new ArgumentException("Máximo 10 caracteres.");
        }

        return new DocumentTypeCode(t);
    }
}
