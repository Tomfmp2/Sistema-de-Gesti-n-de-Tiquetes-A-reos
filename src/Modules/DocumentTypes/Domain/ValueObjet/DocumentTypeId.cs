namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.ValueObjet;

public sealed class DocumentTypeId
{
    public int Value { get; }

    public DocumentTypeId(int value) => Value = value;

    public static DocumentTypeId Unpersisted => new(0);

    public static DocumentTypeId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new DocumentTypeId(value);
    }
}
