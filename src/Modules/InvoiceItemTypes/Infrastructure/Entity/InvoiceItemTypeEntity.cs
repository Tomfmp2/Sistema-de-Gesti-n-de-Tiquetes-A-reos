using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.Entity;

public class InvoiceItemTypeEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<InvoiceItemEntity> InvoiceItems { get; set; } = new List<InvoiceItemEntity>();
}
