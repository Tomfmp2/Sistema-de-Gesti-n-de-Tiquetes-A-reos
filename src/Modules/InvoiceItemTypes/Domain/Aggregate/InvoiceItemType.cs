using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Aggregate;

public class InvoiceItemType
{
    public InvoiceItemTypeId Id { get; private set; }
    public InvoiceItemTypeName Name { get; private set; }

    private InvoiceItemType(
        InvoiceItemTypeId id,
        InvoiceItemTypeName name
    )
    {
        Id = id;
        Name = name;
    }

    public static InvoiceItemType Create(
        InvoiceItemTypeId id,
        InvoiceItemTypeName name
    )
    {
        return new InvoiceItemType(
            id,
            name
        );
    }
}
