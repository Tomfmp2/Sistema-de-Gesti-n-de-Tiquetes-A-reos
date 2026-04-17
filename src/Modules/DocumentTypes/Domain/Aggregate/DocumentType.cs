using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Domain.Aggregate;

public sealed class DocumentType
{
    public DocumentTypeId Id { get; private set; }
    public DocumentTypeName Name { get; private set; }
    public DocumentTypeCode Code { get; private set; }

    private DocumentType(DocumentTypeId id, DocumentTypeName name, DocumentTypeCode code)
    {
        Id = id;
        Name = name;
        Code = code;
    }

    public static DocumentType CreateNew(DocumentTypeName name, DocumentTypeCode code)
    {
        return new DocumentType(DocumentTypeId.Unpersisted, name, code);
    }

    public static DocumentType Create(DocumentTypeId id, DocumentTypeName name, DocumentTypeCode code)
    {
        return new DocumentType(id, name, code);
    }
}
